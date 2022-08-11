namespace CustomerManagement.Infrastructure.Data.Repositories;

using AspNetCore.IQueryable.Extensions;
using CustomerManagement.Domain.Common;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Features.Customers;
using CustomerManagement.Domain.Interfaces.Repository;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    private readonly ILogger<CustomerRepository> logger;
    const string NotFoundMessage = "No Customer was found.";
    const string DuplicateKey = "Cannot insert duplicate key";

    public CustomerRepository(CustomerManagementDbContext dbContext, ILogger<CustomerRepository> logger) : base(dbContext)
    {
        this.logger = logger;
    }

    public async Task<Result<List<Customer>>> GetFiltered(GetCustomerFilterQuery getCustomerFilterQuery)
    {
        try
        {
            var customers = await this._dbContext.Customers.AsQueryable()
                .Apply(getCustomerFilterQuery).ToListAsync();            

            if (!customers.Any())
            {
                this.logger.LogInformation(NotFoundMessage, () => JsonSerializer.Serialize(getCustomerFilterQuery));

                return Result.Fail(Errors.General.NotFound("No Customer was found."));
            }

            return Result.Ok(customers);
        }
        catch (Exception ex)
        {            
            return LogAndReturnGenericErrorMessage(ex, "Error getting Customer by Filters");
        }
    }

    public async Task<Result<Customer>> GetById(Guid customerId)
    {
        try
        {
            var customer = await this.FirstOrDefaultAsync(customerId);

            if (customer is null)
            {
                this.logger.LogInformation(NotFoundMessage, customerId);

                return Result.Fail(Errors.General.NotFound(NotFoundMessage));
            }

            return Result.Ok(customer);
                
        }
        catch (Exception ex)
        {
            return LogAndReturnGenericErrorMessage(ex, "Error getting Customer by customerId", () => new { customerId });
        }
    }

    public async Task<Result<Guid>> Save(Customer customer, bool shoudSaveChanges)
    {
        try
        {
            await base.SaveAsync(customer, shoudSaveChanges);            

            return Result.Ok(customer.Id).WithSuccess(Successes.General.Created("Customer saved with success."));
        }
        catch (Exception ex)
        {
            if (ex.InnerException.Message.Contains(DuplicateKey))
            {
                return LogAndReturnAlreadyExistErrorMessage(ex, "This Email already exists", customer.Email);
            }

            return LogAndReturnGenericErrorMessage(ex, "Error saving Customer", () => new { CustomerId = customer.Id });
        }
    }

    private Result LogAndReturnGenericErrorMessage(Exception ex, string errorMessage, params object?[] args)
    {
        this.logger.LogError(errorMessage, ex, args);

        return Result.Fail(new Error(errorMessage).CausedBy(ex));
    }

    private Result LogAndReturnAlreadyExistErrorMessage(Exception ex, string errorMessage, params object?[] args)
    {
        this.logger.LogError(errorMessage, ex, args);

        return Result.Fail(Errors.General.AlreadyExists(errorMessage).CausedBy(ex));
    }
}
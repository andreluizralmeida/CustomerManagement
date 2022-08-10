namespace CustomerManagement.IntegrationTest.Fakes;

using CustomerManagement.Domain.Common;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Features.Customers;
using CustomerManagement.Domain.Interfaces.Repository;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FakeCustomerRepository : ICustomerRepository
{
    public List<Customer> Customers { get; set; } = new List<Customer>();

    public FakeCustomerRepository()
    {
        Customers.AddRange(GetDefaultCustomers());
    }

    public Task DeleteAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Customer>> GetById(Guid customerId)
    {
        var customer = Customers.SingleOrDefault(p => p.Id == customerId);        

        if (customer is null)
        {
            return Task.FromResult(
                Result.Fail(Errors.General.NotFound("No Customer was found.")).ToResult(new Customer())
                    );
        }
        
        return Task.FromResult(
            Result.Ok(Customers.SingleOrDefault(p => p.Id == customerId)));
    }

    public Task<Result<List<Customer>>> GetFiltered(GetCustomerFilterQuery getCustomerFilterQuery)
    {
        try
        {
            if (!string.IsNullOrEmpty(getCustomerFilterQuery.FirstName))
            {
                Customers = Customers
                    .Where(p => p.FirstName.Contains(getCustomerFilterQuery.FirstName)).ToList();
            }

            if (!string.IsNullOrEmpty(getCustomerFilterQuery.Surname))
            {
                Customers = Customers
                    .Where(p => p.Surname.Contains(getCustomerFilterQuery.Surname)).ToList(); ;
            }

            if (!string.IsNullOrEmpty(getCustomerFilterQuery.Email))
            {
                Customers = Customers
                    .Where(p => p.Email.Contains(getCustomerFilterQuery.Email)).ToList(); ;
            }

            Customers
                .Skip(getCustomerFilterQuery.Offset.Value - 1)
                .Take(getCustomerFilterQuery.Limit.Value);

            if (!Customers.Any())
            {
                return Task.FromResult(
                    Result.Fail(Errors.General.NotFound("No Customer was found.")).ToResult(Customers)
                    );
            }

            return Task.FromResult(Result.Ok(Customers.ToList()));
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    public Task<IEnumerable<Customer>> ListAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<Guid>> Save(Customer customer, bool shoudSaveChanges)
    {
        customer.Id = Guid.NewGuid();
        Customers.Add(customer);

        return Task.FromResult(
            Result.Ok(customer.Id).WithSuccess(Successes.General.Created("Execute action request created with success."))
            );
    }

    public Task SaveAsync(Customer entity, bool shoudSaveChanges)
    {
        throw new NotImplementedException();
    }

    private List<Customer> GetDefaultCustomers()
    {
        return new List<Customer>()
        {
            new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Andre",
                Surname = "Almeida",
                Email = "Andre@Almeida.com",
                Password = "ABC123"
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Andre",
                Surname = "Reis",
                Email = "Helena@Reis.com",
                Password = "ABC123"
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Andre",
                Surname = "Chaves",
                Email = "Maria@Chaves.com",
                Password = "ABC123"
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                Surname = "Cezar",
                Email = "Alice@Cezar.com",
                Password = "ABC123"
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Gabriel",
                Surname = "Almeida",
                Email = "Gabriel@Almeida.com",
                Password = "ABC123"
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Felipe",
                Surname = "Silva",
                Email = "Felipe@Silva.com",
                Password = "ABC123"
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Lucas",
                Surname = "Gomes",
                Email = "Lucas@Gomes.com",
                Password = "ABC123"
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Beth",
                Surname = "Santos",
                Email = "Beth@Santos.com",
                Password = "ABC123"
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Mateus",
                Surname = "Silva",
                Email = "Mateus@Silva.com",
                Password = "ABC123"
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "João",
                Surname = "Almeida",
                Email = "João@Almeida.com",
                Password = "ABC123"
            }
        };
    }
}
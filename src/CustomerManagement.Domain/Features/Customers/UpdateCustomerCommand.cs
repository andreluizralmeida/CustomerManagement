namespace CustomerManagement.Domain.Features.Customers;

using CustomerManagement.Domain.Conventions.Commands;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces.Repository;
using FluentResults;
using Flunt.Validations;
using System.Threading;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

public class UpdateCustomerCommand : Command<Result>
{
    public UpdateCustomerCommand(Guid customerId, string firstName, string surname, string email, string password)
    {
        AddNotifications(new Contract()
            .AreNotEquals(
                Guid.Empty,
                customerId,
                nameof(customerId),
                $"Property {nameof(customerId)} is empty,")
            );

        if (string.IsNullOrEmpty(firstName) && 
            string.IsNullOrEmpty(surname) && 
            string.IsNullOrEmpty(email) && 
            string.IsNullOrEmpty(password))
        {
            AddNotification("All property are null or empty", "At least one property must be changed");
        }

        this.CustomerId = customerId;
        FirstName = firstName;
        Surname = surname;
        Email = email;
        Password = password;
    }

    public Guid CustomerId { get; }
    public string FirstName { get; }
    public string Surname { get; }
    public string Email { get; }
    public string Password { get; }
}

public class UpdateCustomerCommandHandler : CommandHandler<UpdateCustomerCommand, Result>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public override async Task<Result> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var getResult = await _customerRepository.GetById(command.CustomerId);

        if (getResult.IsFailed)
        {
            return getResult.ToResult();
        }

        var customer = this.UpdateCustomer(command, getResult.Value);

        var saveResult = await _customerRepository.Save(customer, true);

        if (saveResult.IsFailed)
        {
            return saveResult.ToResult();
        }

        return Result.Ok();
    }

    private Customer UpdateCustomer(UpdateCustomerCommand command, Customer customer)
    {
        if (!string.IsNullOrEmpty(command.FirstName))
        {
            customer.FirstName = command.FirstName;
        }

        if (!string.IsNullOrEmpty(command.Surname))
        {
            customer.Surname = command.Surname;
        }

        if (!string.IsNullOrEmpty(command.Email))
        {
            customer.Email = command.Email;
        }

        if (!string.IsNullOrEmpty(command.Password))
        {
            customer.Password = BCryptNet.HashPassword(command.Password);
        }

        return customer;
    }
}

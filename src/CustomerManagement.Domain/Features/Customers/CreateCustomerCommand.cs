namespace CustomerManagement.Domain.Features.Customers;

using AutoMapper;
using CustomerManagement.Domain.Conventions.Commands;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces.Repository;
using FluentResults;
using Flunt.Validations;
using System.Threading;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

public class CreateCustomerCommand : Command<Result<Guid>>
{
    public CreateCustomerCommand(string firstName, string surname, string email, string password)
    {
        AddNotifications(new Contract()
            .IsNotNullOrEmpty(firstName, nameof(firstName), "First Name is required")
            .IsNotNullOrEmpty(surname, nameof(surname), "Surname is required")
            .IsNotNullOrEmpty(email, nameof(email), "Email is required")
            .IsNotNullOrEmpty(password, nameof(password), "Password is required")
            .IsEmail(email, nameof(email), "Invalid Email")); 

        FirstName = firstName;
        Surname = surname;
        Email = email;
        Password = password;
    }

    public string FirstName { get; }        
    public string Surname { get; }        
    public string Email { get; }        
    public string Password { get; }
}

public class CreateCustomerCommandHandler : CommandHandler<CreateCustomerCommand, Result<Guid>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public override async Task<Result<Guid>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(command);
        customer.Password = BCryptNet.HashPassword(command.Password);

        return await _customerRepository.Save(customer, true);
    }
}
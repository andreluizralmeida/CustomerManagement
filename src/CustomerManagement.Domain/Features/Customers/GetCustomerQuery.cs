namespace CustomerManagement.Domain.Features.Customers;

using AutoMapper;
using CustomerManagement.Domain.Conventions.Queries;
using CustomerManagement.Domain.Interfaces.Repository;
using CustomerManagement.Domain.Models;
using FluentResults;
using Flunt.Validations;
using System.Threading;
using System.Threading.Tasks;

public class GetCustomerQuery : Query<Result<CustomerModel>>
{
    public GetCustomerQuery(Guid customerId)
    {
        AddNotifications(new Contract()
            .AreNotEquals(
                Guid.Empty,
                customerId,
                nameof(customerId),
                $"Property {nameof(customerId)} is empty,")
            );

        this.CustomerId = customerId;
    }

    public Guid CustomerId { get; }
}

public class GetCustomerQueryHandler : QueryHandler<GetCustomerQuery, Result<CustomerModel>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper) 
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public override async Task<Result<CustomerModel>> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
    {
        var result = await _customerRepository.GetById(query.CustomerId);

        if (result.IsFailed)
        {
            return result.ToResult();
        }

        var customerModel = _mapper.Map<CustomerModel>(result.Value);        

        return Result.Ok(customerModel);
    }
}
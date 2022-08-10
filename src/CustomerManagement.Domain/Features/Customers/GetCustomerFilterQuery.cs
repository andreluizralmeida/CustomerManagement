namespace CustomerManagement.Domain.Features.Customers;

using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using AutoMapper;
using CustomerManagement.Domain.Conventions.Queries;
using CustomerManagement.Domain.Interfaces.Repository;
using CustomerManagement.Domain.Models;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;

public class GetCustomerFilterQuery : Query<Result<IEnumerable<CustomerModel>>>, ICustomQueryable, IQueryPaging, IQuerySort
{
    [QueryOperator(Operator = WhereOperator.Contains)]
    public string FirstName { get; set; }

    [QueryOperator(Operator = WhereOperator.Contains)]
    public string Surname { get; set; }
    public string Email { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string Sort { get; set; }
}

public class GetCustomerFilterQueryHandler : QueryHandler<GetCustomerFilterQuery, Result<IEnumerable<CustomerModel>>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerFilterQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public override async Task<Result<IEnumerable<CustomerModel>>> Handle(GetCustomerFilterQuery query, CancellationToken cancellationToken)
    {
        var result = await _customerRepository.GetFiltered(query);

        if (result.IsFailed)
        {
            return result.ToResult();
        }

        var customerModels = _mapper.Map<IEnumerable<CustomerModel>>(result.Value);

        return Result.Ok(customerModels);
    }
}
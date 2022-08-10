namespace CustomerManagement.Api.Controllers.v1;

using AutoMapper;
using CustomerManagement.Api.Extensions;
using CustomerManagement.Domain.Features.Customers;
using CustomerManagement.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;    
    private readonly IMapper _mapper;

    public CustomerController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;        
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFiltered([FromQuery]SearchCustomer query)
    {
        var GetCustomerFilterQuery = _mapper.Map<GetCustomerFilterQuery>(query);

        var result = await _mediator.Send(GetCustomerFilterQuery);

        return result.AsActionResult();
    }

    [HttpGet("{customerId:Guid}", Name = "GetCustomerById")]    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid customerId)
    {
        var getCustomerQuery = new GetCustomerQuery(customerId);

        if (getCustomerQuery.Invalid)
        {
            return BadRequest(getCustomerQuery.Notifications);
        }

        var result = await _mediator.Send(getCustomerQuery);
        return result.AsActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]    
    public async Task<IActionResult> Save(CustomerModel customerModel)
    {
        var createCustomerCommand = _mapper.Map<CreateCustomerCommand>(customerModel);

        if (createCustomerCommand.Invalid)
        {
            return BadRequest(createCustomerCommand.Notifications);
        }

        var result = await _mediator.Send(createCustomerCommand);

        return result.AsActionResult("GetCustomerById", new { customerId = result.Value });
    }

    [HttpPatch("{customerId:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(Guid customerId, CustomerModel customerModel)
    {
        customerModel.CustomerId = customerId;
        var createCustomerCommand = _mapper.Map<UpdateCustomerCommand>(customerModel);

        if (createCustomerCommand.Invalid)
        {
            return BadRequest(createCustomerCommand.Notifications);
        }

        var result = await _mediator.Send(createCustomerCommand);

        return result.AsActionResult();
    }
}

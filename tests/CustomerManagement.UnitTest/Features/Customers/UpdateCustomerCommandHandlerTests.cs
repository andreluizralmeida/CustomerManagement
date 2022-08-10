namespace CustomerManagement.UnitTest.Features.Customers;

using AutoFixture;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Features.Customers;
using CustomerManagement.Domain.Interfaces.Repository;
using FluentAssertions;
using FluentResults;
using NSubstitute;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

[ExcludeFromCodeCoverage]
public class UpdateCustomerCommandHandlerTests : TestsBase
{

    private readonly ICustomerRepository _customerRepository;    
    private readonly UpdateCustomerCommandHandler _handler;

    public UpdateCustomerCommandHandlerTests()
    {
        _customerRepository = this.Fixture.Freeze<ICustomerRepository>();        
        _handler = this.Fixture.Create<UpdateCustomerCommandHandler>();
    }

    [Fact]
    public async Task Handle_ValidCustomer_ReturnSuccess()
    {
        //Arrange
        var query = this.Fixture.Create<UpdateCustomerCommand>();
        var customer = new Customer()
        {
            Id = query.CustomerId,
            Email = query.Email,
            FirstName = query.FirstName,
            Surname = query.Surname,
            Password = query.Surname
        };

        this._customerRepository.GetById(Arg.Any<Guid>()).Returns(Result.Ok(customer));
        this._customerRepository.Save(Arg.Any<Customer>(), Arg.Any<bool>()).Returns(Result.Ok(query.CustomerId));

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        await _customerRepository.Received(1).GetById(Arg.Any<Guid>());        
        await _customerRepository.Received(1).Save(Arg.Any<Customer>(), Arg.Any<bool>());
    }

    [Fact]
    public async Task Handle_ValidUnknowCustomerGetFail_ReturnFail()
    {
        //Arrange
        var query = this.Fixture.Create<UpdateCustomerCommand>();

        this._customerRepository.GetById(Arg.Any<Guid>()).Returns(Result.Fail(new Error("")));        

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsFailed.Should().BeTrue();
        await _customerRepository.Received(1).GetById(Arg.Any<Guid>());
        await _customerRepository.Received(0).Save(Arg.Any<Customer>(), Arg.Any<bool>());
    }

    [Fact]
    public async Task Handle_ValidUnknowCustomerSaveErrror_ReturnFail()
    {
        //Arrange
        var query = this.Fixture.Create<UpdateCustomerCommand>();
        var customer = new Customer()
        {
            Id = query.CustomerId,
            Email = query.Email,
            FirstName = query.FirstName,
            Surname = query.Surname,
            Password = query.Surname
        };

        this._customerRepository.GetById(Arg.Any<Guid>()).Returns(Result.Ok(customer));
        this._customerRepository.Save(Arg.Any<Customer>(), Arg.Any<bool>()).Returns(Result.Fail(new Error("")));

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.IsFailed.Should().BeTrue();
        await _customerRepository.Received(1).GetById(Arg.Any<Guid>());
        await _customerRepository.Received(1).Save(Arg.Any<Customer>(), Arg.Any<bool>());
    }
}
namespace CustomerManagement.UnitTest.Features.Customers
{
    using AutoFixture;
    using AutoMapper;
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
    public class CreateCustomerCommandHandlerTests : TestsBase
    {

		private readonly ICustomerRepository _customerRepository;
		private readonly IMapper _mapper;
		private readonly CreateCustomerCommandHandler _handler;

		public CreateCustomerCommandHandlerTests()
        {
			_customerRepository = this.Fixture.Freeze<ICustomerRepository>();
			_mapper = this.Fixture.Freeze<IMapper>();
			_handler = this.Fixture.Create<CreateCustomerCommandHandler>();
		}

		[Fact]
		public async Task Handle_ValidCustomer_SuccessSave()
        {
			//Arrange
			var customerId = new Guid();
			var query = this.Fixture.Create<CreateCustomerCommand>();
			var customer = this.Fixture.Create<Customer>();

			_mapper.Map<Customer>(Arg.Any<CreateCustomerCommand>()).Returns(customer);
			_customerRepository.Save(Arg.Any<Customer>(), Arg.Any<bool>()).Returns(Result.Ok(customerId));

			//Act
			var result = await _handler.Handle(query, CancellationToken.None);

			//Assert
			result.IsSuccess.Should().BeTrue();
			result.Value.Should().Be(customerId);
			_mapper.Received(1).Map<Customer>(Arg.Any<CreateCustomerCommand>());
			await _customerRepository.Received(1).Save(Arg.Any<Customer>(), Arg.Any<bool>());
		}

		[Fact]
		public async Task Handle_ValidCustomer_ReturnResultFail()
		{
			//Arrange
			var customerId = new Guid();
			var query = this.Fixture.Create<CreateCustomerCommand>();
			var customer = this.Fixture.Create<Customer>();

			_mapper.Map<Customer>(Arg.Any<CreateCustomerCommand>()).Returns(customer);
			_customerRepository.Save(Arg.Any<Customer>(), Arg.Any<bool>()).Returns(Result.Fail(new Error("")));

			//Act
			var result = await _handler.Handle(query, CancellationToken.None);

			//Assert
			result.IsFailed.Should().BeTrue();			
			_mapper.Received(1).Map<Customer>(Arg.Any<CreateCustomerCommand>());
			await _customerRepository.Received(1).Save(Arg.Any<Customer>(), Arg.Any<bool>());
		}
	}
}

namespace CustomerManagement.UnitTest.Features.Customers
{
    using AutoFixture;
    using AutoMapper;
    using CustomerManagement.Domain.Entities;
    using CustomerManagement.Domain.Features.Customers;
    using CustomerManagement.Domain.Interfaces.Repository;
    using CustomerManagement.Domain.Models;
    using FluentAssertions;
    using FluentResults;
    using NSubstitute;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class GetCustomerQueryHandlerTests : TestsBase
    {
		private readonly ICustomerRepository _customerRepository;
		private readonly IMapper _mapper;
		private readonly GetCustomerQueryHandler _handler;

		public GetCustomerQueryHandlerTests()
        {
			_customerRepository = this.Fixture.Freeze<ICustomerRepository>();
			_mapper = this.Fixture.Freeze<IMapper>();
			_handler = this.Fixture.Create<GetCustomerQueryHandler>();
		}

		[Fact]
		public async Task Handle_ValidCustomerFilter_Success()
        {
			//Arrange
			var query = this.Fixture.Create<GetCustomerQuery>();
			var customer = this.Fixture.Create<Customer>();
			var customerModel = new CustomerModel()
			{
				FirstName = customer.FirstName,
				Surname = customer.Surname,
				Email = customer.Email,
				CustomerId = customer.Id
			};

			this._customerRepository.GetById(Arg.Any<Guid>()).Returns(Result.Ok(customer));
			_mapper.Map<CustomerModel>(Arg.Any<Customer>()).Returns(customerModel);

			//Act
			var result = await _handler.Handle(query, CancellationToken.None);

			//Assert
			result.IsSuccess.Should().BeTrue();

			result.Value.FirstName.Should().Be(customer.FirstName);
			result.Value.Surname.Should().Be(customer.Surname);
			result.Value.Email.Should().Be(customer.Email);
			result.Value.Password.Should().BeNull();

			await _customerRepository.Received(1).GetById(Arg.Any<Guid>());
			_mapper.Received(1).Map<CustomerModel>(Arg.Any<Customer>());
		}

		[Fact]
		public async Task Handle_ValidCustomerIdButNoCustomerOnDatabase_ReturnError()
		{
			//Arrange
			var query = this.Fixture.Create<GetCustomerQuery>();
			var customers = this.Fixture.CreateMany<Customer>(3).ToList();

			this._customerRepository.GetById(Arg.Any<Guid>()).Returns(Result.Fail(new Error("")));			

			//Act
			var result = await _handler.Handle(query, CancellationToken.None);

			//Assert
			result.IsFailed.Should().BeTrue();
			await _customerRepository.Received(1).GetById(Arg.Any<Guid>());
			_mapper.Received(0).Map<CustomerModel>(Arg.Any<Customer>());
		}
	}
}
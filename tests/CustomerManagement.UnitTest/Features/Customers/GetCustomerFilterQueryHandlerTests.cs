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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class GetCustomerFilterQueryHandlerTests : TestsBase
    {
		private readonly ICustomerRepository _customerRepository;
		private readonly IMapper _mapper;
		private readonly GetCustomerFilterQueryHandler _handler;

		public GetCustomerFilterQueryHandlerTests()
        {
			_customerRepository = this.Fixture.Freeze<ICustomerRepository>();
			_mapper = this.Fixture.Freeze<IMapper>();
			_handler = this.Fixture.Create<GetCustomerFilterQueryHandler>();
		}

		[Fact]
		public async Task Handle_ValidCustomerFilter_ReturnCustomersWithSuccess()
        {
			//Arrange
			var query = this.Fixture.Create<GetCustomerFilterQuery>();
			var customers = this.Fixture.CreateMany<Customer>(3).ToList();
			var customerModels = this.Fixture.CreateMany<CustomerModel>(3);

			_customerRepository.GetFiltered(Arg.Any<GetCustomerFilterQuery>()).Returns(Result.Ok(customers));
			_mapper.Map<IEnumerable<CustomerModel>>(Arg.Any<List<Customer>>()).Returns(customerModels);

			//Act
			var result = await _handler.Handle(query, CancellationToken.None);

			//Assert
			result.IsSuccess.Should().BeTrue();
			result.Value.Count().Should().Be(3);
			await _customerRepository.Received(1).GetFiltered(Arg.Any<GetCustomerFilterQuery>());
			_mapper.Received(1).Map<IEnumerable<CustomerModel>>(Arg.Any<List<Customer>>());
		}

		[Fact]
		public async Task Handle_ValidCustomerFilterButNoCustomerOnDatabase_ReturnError()
		{
			//Arrange
			var query = this.Fixture.Create<GetCustomerFilterQuery>();
			var customers = this.Fixture.CreateMany<Customer>(3).ToList();

			this._customerRepository.GetFiltered(Arg.Any<GetCustomerFilterQuery>()).Returns(Result.Fail(new Error("")));

			//Act
			var result = await _handler.Handle(query, CancellationToken.None);

			//Assert
			result.IsFailed.Should().BeTrue();

			await _customerRepository.Received(1).GetFiltered(Arg.Any<GetCustomerFilterQuery>());
			_mapper.Received(0).Map<IEnumerable<CustomerModel>>(Arg.Any<List<Customer>>());
		}
	}
}
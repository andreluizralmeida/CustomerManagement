namespace CustomerManagement.IntegrationTest.Controllers;

using CustomerManagement.IntegrationTest.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

[ExcludeFromCodeCoverage]
public class CustomerControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public CustomerControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    //GET

    [Fact]
    public async Task GetFiltered_WithValidCustomerFirstName_StatusCodeOk()
    {
        var customer = _factory.FakeCustomerRepository.Customers.First();        

        var response = await _client.GetAsync(
            $"v1/customer?FirstName={customer.FirstName}"
            );

        response.StatusCode.Should().Be(HttpStatusCode.OK);        
    }

    [Fact]
    public async Task GetFiltered_WithValidCustomerFirstName_ExaclyNumberOfCustomers()
    {
        const string FirstName = "Andre";

        var fistNameCount = _factory.FakeCustomerRepository.Customers
            .Where(c => c.FirstName == FirstName).Count();

        var response = await _client.GetFromJsonAsync<TestCustomerInputModel[]>(
            $"v1/customer?FirstName={FirstName}"
            );

        response.Count().Should().Be(fistNameCount);
    }

    public async Task GetById_WithValidCustomerId_SuccessReturnsExpectedCustomer()
    {
        var customer = _factory.FakeCustomerRepository.Customers.First();

        var content = GetValidProductJsonContent();

        var response = await _client.GetAsync($"v1/customer/{customer.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_WithValidCustomerId_ExaclyTheSameCustomers()
    {
        var customer = _factory.FakeCustomerRepository.Customers.First();

        var response = await _client.GetFromJsonAsync<TestCustomerInputModel>($"v1/customer/{customer.Id}");

        response.FirstName.Should().Be(customer.FirstName);
        response.Surname.Should().Be(customer.Surname);
        response.Email.Should().Be(customer.Email);
        new Guid(response.CustomerId).Should().Be(customer.Id);
    }

    //POST

    [Fact]
    public async Task Post_WithValidCustomer_SuccessReturnsExpectedProblemDetails()
    {        
        var content = GetValidProductJsonContent();

        var response = await _client.PostAsync("v1/customer", content);

        var customer = _factory.FakeCustomerRepository.Customers.FirstOrDefault(c => c.Email == "teste@teste.com");

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        customer.Should().NotBeNull();
    }

    [Theory]
    [MemberData(nameof(GetInvalidInputsAndProblemDetailsErrorValidator))]
    public async Task Post_WithInvalidName_ReturnsExpectedProblemDetails(TestCustomerInputModel customerModel)
    {
        var response = await _client.PostAsync("v1/customer", JsonContent.Create(customerModel));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);        
    }

    private static JsonContent GetValidProductJsonContent(Guid? customerId = null)
    {
        return JsonContent.Create(GetValidCustomerInputModel(customerId));
    }

    //PATCH

    [Fact]
    public async Task Patch_WithValidCustomer_SuccessWithAllPropertiesUpdated()
    {
        var customerPatch = _factory.FakeCustomerRepository.Customers.First();

        customerPatch.FirstName = "IntegrationTest";
        customerPatch.Surname = "IntegrationTest";
        customerPatch.Email = "IntegrationTest";
        customerPatch.Password = "IntegrationTest";

        var response = await _client.PatchAsync($"v1/customer/{customerPatch.Id}", JsonContent.Create(customerPatch));

        var customer = _factory.FakeCustomerRepository.Customers.FirstOrDefault(c => c.Email == "IntegrationTest");

        customer.FirstName.Should().Be("IntegrationTest");
        customer.Surname.Should().Be("IntegrationTest");
        customer.Email.Should().Be("IntegrationTest");
        customer.Password.Should().NotBe("IntegrationTest");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Patch_AllPropertiesEmpty_BadRequest()
    {
        var customerPatch = _factory.FakeCustomerRepository.Customers.First();

        customerPatch.FirstName = "";
        customerPatch.Surname = "";
        customerPatch.Email = "";
        customerPatch.Password = "";

        var response = await _client.PatchAsync($"v1/customer/{customerPatch.Id}", JsonContent.Create(customerPatch));

        var customer = _factory.FakeCustomerRepository.Customers.FirstOrDefault(c => c.Email == "IntegrationTest");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Patch_UnkownId_NotFound()
    {
        var newGuid = Guid.NewGuid();
        var content = GetValidProductJsonContent(newGuid);

        var response = await _client.PatchAsync($"v1/customer/{newGuid}", content);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    //Private

    private static TestCustomerInputModel GetValidCustomerInputModel(Guid? customerId = null)
    {
        return new TestCustomerInputModel
        {
            CustomerId = (customerId ?? Guid.NewGuid()).ToString(),
            FirstName = "FirstName",
            Surname = "SurName",
            Email = "teste@teste.com",
            Password = "ABC123",
        };
    }

    private static IEnumerable<object[]> GetInvalidInputsAndProblemDetailsErrorValidator()
    {
        var testData = new List<object[]>
            {
                new object[]
                {
                    GetValidCustomerInputModel().CloneWith(x => x.FirstName = null)                    
                },
                new object[]
                {
                    GetValidCustomerInputModel().CloneWith(x => x.Surname = null)                    
                },
                new object[]
                {
                    GetValidCustomerInputModel().CloneWith(x => x.Email = null)                   
                },
                new object[]
                {
                    GetValidCustomerInputModel().CloneWith(x => x.Password = null)                    
                }
            };

        return testData;
    }
}
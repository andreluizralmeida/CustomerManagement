namespace CustomerManagement.IntegrationTest;

using CustomerManagement.Domain.Interfaces.Repository;
using CustomerManagement.IntegrationTest.Fakes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    public FakeCustomerRepository FakeCustomerRepository { get; }

    public CustomWebApplicationFactory()
    {
        FakeCustomerRepository = new FakeCustomerRepository();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<ICustomerRepository>(FakeCustomerRepository);
        });
    }
}
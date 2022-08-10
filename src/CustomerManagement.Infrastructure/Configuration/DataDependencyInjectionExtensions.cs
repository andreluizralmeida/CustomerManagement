namespace CustomerManagement.Infrastructure.Configuration;

using CustomerManagement.Domain.Interfaces.Repository;
using CustomerManagement.Infrastructure.Data;
using CustomerManagement.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

public static class DataDependencyInjectionExtensions
{
    public static IServiceCollection ConfigureData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddDbContext<CustomerManagementDbContext>(
            options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("CustomerManagementSqlServer"),
                    options => options.EnableRetryOnFailure());
                options.EnableSensitiveDataLogging();
            }, optionsLifetime: ServiceLifetime.Transient);

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
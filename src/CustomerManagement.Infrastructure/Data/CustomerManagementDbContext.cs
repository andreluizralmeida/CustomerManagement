namespace CustomerManagement.Infrastructure.Data;

using CustomerManagement.Domain.Entities;
using CustomerManagement.Infrastructure.Data.EntityMap;
using Microsoft.EntityFrameworkCore;

public class CustomerManagementDbContext : DbContext
{
    public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options) : base(options)
    {
    }   

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfiguration(new CustomerEntityMap());

        base.OnModelCreating(modelBuilder);
    }
}
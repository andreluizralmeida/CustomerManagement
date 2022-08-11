namespace CustomerManagement.Infrastructure.Data.EntityMap
{
    using CustomerManagement.Domain.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CustomerEntityMap : DbEntityConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.HasIndex(c => c.FirstName);
            entity.HasIndex(c => c.Email).IsUnique();
            entity.Property(c => c.Surname).HasMaxLength(50);
            entity.Property(c => c.FirstName).HasMaxLength(50);
            entity.Property(c => c.Email).HasMaxLength(100);            
        }
    }
}
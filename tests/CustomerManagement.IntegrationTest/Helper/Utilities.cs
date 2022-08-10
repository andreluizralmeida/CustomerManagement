namespace CustomerManagement.IntegrationTest.Helper
{
    using CustomerManagement.Domain.Entities;
    using CustomerManagement.Infrastructure.Data;

    public class Utilities
    {
        public static void InitializeDbForTests(CustomerManagementDbContext context)
        {
            context.Customers.Add(new Customer()
            {
                FirstName = "Andre",
                Surname = "Almeida",
                Email = "Andre@Almeida.com",
                Password = "ABC123",
            });

            context.Customers.Add(new Customer()
            {
                FirstName = "Andre",
                Surname = "Reis",
                Email = "Helena@Reis.com",
                Password = "ABC123",
            });

            context.Customers.Add(new Customer()
            {
                FirstName = "Andre",
                Surname = "Chaves",
                Email = "Maria@Chaves.com",
                Password = "ABC123",
            });

            context.Customers.Add(new Customer()
            {
                FirstName = "Alice",
                Surname = "Cezar",
                Email = "Alice@Cezar.com",
                Password = "ABC123",
            });

            context.Customers.Add(new Customer()
            {
                FirstName = "Gabriel",
                Surname = "Almeida",
                Email = "Gabriel@Almeida.com",
                Password = "ABC123",
            });

            context.Customers.Add(new Customer()
            {
                FirstName = "Felipe",
                Surname = "Silva",
                Email = "Felipe@Silva.com",
                Password = "ABC123",
            });

            context.Customers.Add(new Customer()
            {
                FirstName = "Lucas",
                Surname = "Gomes",
                Email = "Lucas@Gomes.com",
                Password = "ABC123",
            });

            context.Customers.Add(new Customer()
            {
                FirstName = "Beth",
                Surname = "Santos",
                Email = "Beth@Santos.com",
                Password = "ABC123",
            });

            context.Customers.Add(new Customer()
            {
                FirstName = "Mateus",
                Surname = "Silva",
                Email = "Mateus@Silva.com",
                Password = "ABC123",
            });

            context.Customers.Add(new Customer()
            {
                FirstName = "João",
                Surname = "Almeida",
                Email = "João@Almeida.com",
                Password = "ABC123",
            });

            context.SaveChanges();
        }
    }
}
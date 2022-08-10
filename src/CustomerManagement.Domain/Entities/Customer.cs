namespace CustomerManagement.Domain.Entities;

public class Customer : Entity
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
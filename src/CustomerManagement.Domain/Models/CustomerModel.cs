namespace CustomerManagement.Domain.Models;

public class CustomerModel
{
    public Guid? CustomerId { get; set; }
    public string? FirstName { get; set; }    
    public string? Surname { get; set; }    
    public string? Email { get; set; }
    public string? Password { get; set; }
}
namespace CustomerManagement.Api.Extensions;

public class SearchCustomer
{
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public int? Page { get; set; } = 1;
    public int? Size { get; set; } = 10;
    public string? Sort { get; set; }
}
namespace CustomerManagement.IntegrationTest.Models;

using System;

public class TestCustomerInputModel
{
    public string CustomerId { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public TestCustomerInputModel CloneWith(Action<TestCustomerInputModel> changes)
    {
        var clone = (TestCustomerInputModel)MemberwiseClone();

        changes(clone);

        return clone;
    }
}


namespace CustomerManagement.Domain.Interfaces.Repository;

using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Features.Customers;
using FluentResults;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Task<Result<Customer>> GetById(Guid customerId);
    Task<Result<Guid>> Save(Customer customer, bool shoudSaveChanges);
    Task<Result<List<Customer>>> GetFiltered(GetCustomerFilterQuery getCustomerFilterQuery);
}
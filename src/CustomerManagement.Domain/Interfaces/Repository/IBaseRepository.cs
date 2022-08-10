namespace CustomerManagement.Domain.Interfaces.Repository;

using CustomerManagement.Domain.Entities;

public interface IBaseRepository<T> where T : Entity
{
    Task<IEnumerable<T>> ListAllAsync();
    Task SaveAsync(T entity, bool shoudSaveChanges);    
    Task DeleteAsync(T entity);    
}
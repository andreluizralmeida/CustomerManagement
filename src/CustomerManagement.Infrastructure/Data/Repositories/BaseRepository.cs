namespace CustomerManagement.Infrastructure.Data.Repositories;

using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    protected readonly CustomerManagementDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(CustomerManagementDbContext dbContext)
    {
        this._dbContext = dbContext;
        this._dbSet = this._dbContext.Set<T>();
    }

    public async Task<T> FirstOrDefaultAsync(Guid id)
    {
        return await this._dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<T>> ListAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task SaveAsync(T entity, bool shoudSaveChanges)
    {
        if (entity.Id != Guid.Empty)
        {
            _dbSet.Update(entity);
        }
        else
        {
            await _dbSet.AddAsync(entity);
        }

        if (shoudSaveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
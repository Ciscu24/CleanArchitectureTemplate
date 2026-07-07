using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CleanArchitectureTemplate.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll(Expression<Func<T, object>>[] includesProperties = null);
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includesProperties = null);
        Task<T> GetById(object id);
        Task<T> GetFirst(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includes = null);
        Task<bool> Any(Expression<Func<T, bool>> function = null);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(object id);
    }
}

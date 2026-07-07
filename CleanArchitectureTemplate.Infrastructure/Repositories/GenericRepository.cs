using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger;
        }

        #region Métodos públicos

        public async Task<IQueryable<T>> GetAll(Expression<Func<T, object>>[] includesProperties = null)
        {
            try
            {
                var objects = _dbSet.AsQueryable();

                if (includesProperties != null)
                    IncludesProperties(ref objects, includesProperties);

                return objects;
            }
            catch (Exception ex)
            {
                _logger.LogError("GenericRepository.GetAll", ex);
                throw;
            }
        }

        public async Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includesProperties = null)
        {
            try
            {
                IQueryable<T> objects;

                if (filter != null)
                    objects = _dbSet.Where(filter);
                else
                    objects = _dbSet.AsQueryable();

                if (includesProperties != null)
                    IncludesProperties(ref objects, includesProperties);

                return objects;
            }
            catch (Exception ex)
            {
                _logger.LogError("GenericRepository.GetAll", ex);
                throw;
            }
        }

        public async Task<T> GetById(object id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("GenericRepository.Get", ex);
                throw;
            }
        }

        public async Task<T> GetFirst(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includesProperties = null)
        {
            try
            {
                if (includesProperties != null)
                {
                    var objects = _dbSet.Where(filter);
                    IncludesProperties(ref objects, includesProperties);
                    return await objects.FirstOrDefaultAsync();
                }
                else
                    return await _dbSet.FirstOrDefaultAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError("GenericRepository.GetFirst", ex);
                throw;
            }
        }

        public async Task<bool> Any(Expression<Func<T, bool>> function = null)
        {
            try
            {
                if (function != null)
                    return await _dbSet.AnyAsync(function);
                else
                    return await _dbSet.AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("GenericRepository.Any", ex);
                throw;
            }
        }

        public async Task Add(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("GenericRepository.Add", ex);
                throw;
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("GenericRepository.Update", ex);
                throw;
            }
        }

        public async Task Delete(object id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GenericRepository.Remove", ex);
                throw;
            }
        }

        #endregion

        #region Métodos privados

        /// <summary>
        ///     Método que aplica los includes pasados como parámetro al listado de elementos pasado como referencia
        /// </summary>
        /// <param name="list">Listado de elementos</param>
        /// <param name="includesProperties">Includes a aplicar</param>
        /// <returns></returns>
        private IQueryable<T> IncludesProperties(ref IQueryable<T> list, Expression<Func<T, object>>[] includesProperties)
        {
            try
            {
                foreach (var propertie in includesProperties)
                {
                    list = list.Include(propertie);
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}

using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Domain.Models;
using CleanArchitectureTemplate.Infrastructure.Repositories;
using CleanArchitectureTemplate.Web.Data;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.Infrastructure.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        #region Repositorios

        // Variables privadas para almacenar las instancias
        private IGenericRepository<Product> _products;
        // private IUsuarioRepository _usuarios;

        #endregion

        #region Inicialización de Repositorios

        public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context, _logger);

        #endregion

        #region Constructor

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        #endregion

        #region Métodos Públicos

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}

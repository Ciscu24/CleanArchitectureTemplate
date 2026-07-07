using CleanArchitectureTemplate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureTemplate.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        #region Repositorios

        // Añadimos las propiedades para acceder a cada repositorio
        IGenericRepository<Product> Products { get; }
        // IGenericRepository<Usuario> Usuarios { get; }

        #endregion

        Task SaveChanges();
    }
}

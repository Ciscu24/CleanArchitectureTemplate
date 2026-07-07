using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureTemplate.Application.Services
{
    public interface IProductService
    {
        Task CrearProductoAsync(string nombre, decimal precio);
    }

    public sealed class ProductService : BaseService<ProductService>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger): base(unitOfWork, logger){ }

        public async Task CrearProductoAsync(string nombre, decimal precio)
        {
            var nuevoProducto = new Product
            {
                Name = nombre,
                Price = precio,
                CreateDate = DateTime.UtcNow,
                Description = ""
            };

            // 1. Usas el repositorio a través de la UoW para añadir la entidad a memoria
            await _unitOfWork.Products.Add(nuevoProducto);

            // Podrías hacer más cosas aquí, como actualizar el stock, crear un log...
            // await _unitOfWork.Logs.AddAsync(nuevoLog);

            // 2. Guardas TODO en una sola transacción a la base de datos
            await _unitOfWork.SaveChanges();
        }
    }
}

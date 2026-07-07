using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tappa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // La ruta será: api/productos
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // Inyectamos la capa de Aplicación, NUNCA la infraestructura
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto request)
        {
            // 1. Validaciones básicas (Más adelante puedes usar FluentValidation)
            if (string.IsNullOrWhiteSpace(request.Name) || request.Price <= 0)
            {
                return BadRequest("Datos del producto inválidos.");
            }

            try
            {
                // 2. Pasamos el mando a la capa de Aplicación
                await _productService.CrearProductoAsync(request.Name, request.Price);

                // 3. Devolvemos la respuesta de éxito
                return Ok(new { Mensaje = "Producto creado con éxito" });
            }
            catch (Exception ex)
            {
                // Aquí podrías loggear el error
                return StatusCode(500, "Ocurrió un error interno al crear el producto.");
            }
        }
    }
}

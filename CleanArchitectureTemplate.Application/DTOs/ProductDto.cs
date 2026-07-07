using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArchitectureTemplate.Application.DTOs
{
    public class ProductDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio del producto debe ser mayor que cero")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
    }
}

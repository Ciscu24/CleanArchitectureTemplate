using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureTemplate.Domain.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

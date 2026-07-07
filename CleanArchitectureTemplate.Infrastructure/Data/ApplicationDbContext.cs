using CleanArchitectureTemplate.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // 2. Aquí sigues teniendo TUS propios DbSet de tu dominio
        //public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // A partir de aquí, tus configuraciones propias
            // builder.Entity<Product>().Property(p => p.Name).IsRequired();
        }
    }
}


    

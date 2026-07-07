using CleanArchitectureTemplate.Application.Services;
using CleanArchitectureTemplate.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registrar los servicios de la infraestructura (como el DbContext y la Unidad de Trabajo)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

// Estos dos servicios son los que generan la documentación de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Opcional: Puedes cambiar el título y la ruta por defecto
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API Enterprise v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

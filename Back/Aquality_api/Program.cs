// Importación de los espacios de nombres necesarios
using Aquality_api.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

// Crear un nuevo constructor de la aplicación web
var builder = WebApplication.CreateBuilder(args);

// Crear una instancia del contexto de base de datos AqualityContext
AqualityContext database = new AqualityContext();

// Agregar servicios al contenedor de inyección de dependencias
builder.Services.AddControllers();

// Configuración de Swagger/OpenAPI para documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
    // Configuración básica de información para la documentación Swagger
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Aquality",
        Version = "v0.1",
        Description = "Proyecto final",
    }); ;
});

// Construir la aplicación
var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger en entornos de desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurar CORS para permitir solicitudes desde http://localhost:4200
app.UseCors(options =>
    options.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());

// Asegurar que la base de datos esté creada
database.Database.EnsureCreated();

// Redirección HTTPS
app.UseHttpsRedirection();

// Configuración de autorización
app.UseAuthorization();

// Mapear controladores de API
app.MapControllers();

// Ejecutar la aplicación
app.Run();

// Importaci�n de los espacios de nombres necesarios
using Aquality_api.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

// Crear un nuevo constructor de la aplicaci�n web
var builder = WebApplication.CreateBuilder(args);

// Crear una instancia del contexto de base de datos AqualityContext
AqualityContext database = new AqualityContext();

// Agregar servicios al contenedor de inyecci�n de dependencias
builder.Services.AddControllers();

// Configuraci�n de Swagger/OpenAPI para documentaci�n de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
    // Configuraci�n b�sica de informaci�n para la documentaci�n Swagger
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Aquality",
        Version = "v0.1",
        Description = "Proyecto final",
    }); ;
});

// Construir la aplicaci�n
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

// Asegurar que la base de datos est� creada
database.Database.EnsureCreated();

// Redirecci�n HTTPS
app.UseHttpsRedirection();

// Configuraci�n de autorizaci�n
app.UseAuthorization();

// Mapear controladores de API
app.MapControllers();

// Ejecutar la aplicaci�n
app.Run();

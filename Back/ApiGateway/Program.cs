using Ocelot.Middleware;
using Ocelot.DependencyInjection;

// Creación del objeto para construir la aplicación web
var builder = WebApplication.CreateBuilder(args);

// Agregando servicios necesarios para controladores y enrutamiento
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración del archivo de configuración de Ocelot
builder.Configuration.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);

// Agregando el middleware de Ocelot al servicio
builder.Services.AddOcelot();

// Construcción de la aplicación
var app = builder.Build();

// Configuraciones específicas para entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    // Habilitando la interfaz Swagger para documentación
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Utilizando el middleware de Ocelot para la gestión de la puerta de enlace API
await app.UseOcelot();

// Redirección HTTPS
app.UseHttpsRedirection();

// Configuración de autorización
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Ejecución de la aplicación
app.Run();

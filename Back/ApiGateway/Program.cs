using Ocelot.Middleware;
using Ocelot.DependencyInjection;

// Creaci�n del objeto para construir la aplicaci�n web
var builder = WebApplication.CreateBuilder(args);

// Agregando servicios necesarios para controladores y enrutamiento
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n del archivo de configuraci�n de Ocelot
builder.Configuration.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);

// Agregando el middleware de Ocelot al servicio
builder.Services.AddOcelot();

// Construcci�n de la aplicaci�n
var app = builder.Build();

// Configuraciones espec�ficas para entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    // Habilitando la interfaz Swagger para documentaci�n
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Utilizando el middleware de Ocelot para la gesti�n de la puerta de enlace API
await app.UseOcelot();

// Redirecci�n HTTPS
app.UseHttpsRedirection();

// Configuraci�n de autorizaci�n
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Ejecuci�n de la aplicaci�n
app.Run();

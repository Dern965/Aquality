using Aquality_api.Model;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Aquality_api.Context
{
    public class AqualityContext : DbContext
    {
        // Define los conjuntos de entidades para cada modelo.
        public DbSet<CarritoModel> Carritos {  get; set; }
        public DbSet<EspecieModel> Especies { get; set; }
        public DbSet<HistorialModel> Historiales { get; set; }
        public DbSet<OrdenModel> Ordenes { get; set; }
        public DbSet<ProductoModel> Productos { get; set; }
        public DbSet<TiendaModel> Tiendas { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        // Configura las opciones de la conexión a la base de datos.
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            // Configuración para conectar a MySQL.
            //Jurgen
            builder.UseMySQL("server=localhost; database=aquality; user=root; password=Jur63nqetu$");

            //Eduardo
            //builder.UseMySQL("server=localhost; database=aquality; user=root; password=admin");
        }
        // Configura el modelo de datos y las relaciones entre entidades.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de cada entidad con sus propiedades y claves primarias.
            builder.Entity<CarritoModel>(e =>
            {
                e.HasKey(u => u.idCarrito);
                e.Property(u => u.usuario);
                e.Property(u => u.productos);
            });

            // Se repite este patrón para cada entidad en el modelo.
            builder.Entity<EspecieModel>(e =>
            {
                e.HasKey(u => u.especie);
                e.Property(u => u.habitat);
                e.Property(u => u.conservacion);
                e.Property(u => u.adaptabilidad);
                e.Property(u => u.amenazas);
                e.Property(u => u.contaminacion);
                e.Property(u => u.consejos);
                e.Property(u => u.consejos);
                e.Property(u => u.poblacion);
                e.Property(u => u.idProductos);
            });

            builder.Entity<HistorialModel>(e =>
            {
                e.HasKey(u => u.idHistorial);
                e.Property(u => u.idUsuarios);
                e.Property(u => u.idOrdenes);
            });

            builder.Entity<OrdenModel>(e =>
            {
                e.HasKey(u => u.idOrden);
                e.Property(u => u.idCarrito);
                e.Property(u => u.fechaEntrega);
                e.Property(u => u.seguimiento);
            });

            builder.Entity<ProductoModel>(e =>
            {
                e.HasKey(u => u.idProducto);
                e.Property(u => u.nombreProducto);
                e.Property(u => u.descripcion);
                e.Property(u => u.precio);
                e.Property(u => u.fabricacion);
                e.Property(u => u.disponibilidad);
                e.Property(u => u.idTienda);
            });

            builder.Entity<TiendaModel>(e =>
            {
                e.HasKey(u => u.idTienda);
                e.Property(u => u.nombreTienda);
                e.Property(u => u.ubicacion);
                e.Property(u => u.telefono);
                e.Property(u => u.idProductos);
            });

            builder.Entity<UsuarioModel>(e =>
            {
                e.HasKey(u => u.username);
                e.Property(u => u.email);
                e.Property(u => u.password);
                e.Property(u => u.phone);
                e.Property(u => u.first_Name);
                e.Property(u => u.last_Name);
                e.Property(u => u.fnac);
                e.Property(u => u.idHistorial);
            });
        }
    }
}

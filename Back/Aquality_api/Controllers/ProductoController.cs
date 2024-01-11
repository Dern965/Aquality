using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aquality_api.Context;
using Aquality_api.Model;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aquality_api.Controllers
{
    // Especificación de que es un controlador de API
    [ApiController]
    // Ruta base para este controlador
    [Route("ProductosController")]
    public class ProductoController : ControllerBase
    {
        // Método HTTP GET para obtener la lista de productos
        [HttpGet]
        public JsonResult ProductosGet()
        {
            // Lista para almacenar los productos
            List<ProductoModel> productos = new List<ProductoModel>();

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Obtener todos los productos de la base de datos
                var aux = database.Productos;

                // Iterar sobre cada producto y agregarlo a la lista
                foreach (var item in aux)
                {
                    productos.Add(new ProductoModel
                    {
                        idProducto = item.idProducto,
                        nombreProducto = item.nombreProducto,
                        descripcion = item.descripcion,
                        precio = item.precio,
                        fabricacion = item.fabricacion,
                        disponibilidad = item.disponibilidad,
                        idTienda = item.idTienda,
                    });
                }
            }

            // Devolver la lista de productos en formato JSON
            return new JsonResult(productos);
        }

        // Método HTTP POST para agregar un nuevo producto
        [HttpPost]
        public JsonResult ProductosPost([FromBody] ProductoModel productos)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Agregar el nuevo producto a la base de datos
                database.Productos.Add(productos);

                // Guardar los cambios en la base de datos
                database.SaveChanges();

                // La operación fue exitosa
                comprobacion = true;
            }

            // Devolver una respuesta JSON indicando el resultado de la operación
            return new JsonResult(comprobacion);
        }

        // Método HTTP PATCH para actualizar un producto existente
        [HttpPatch]
        public JsonResult ProductosUpdate([FromBody] ProductoModel productos)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Buscar el producto existente por su ID
                var existe = database.Productos.SingleOrDefault(p => p.idProducto == productos.idProducto);

                // Verificar si el producto existe
                if (existe != null)
                {
                    // Cambiar el estado de la entidad existente a desvinculado
                    database.Entry(existe).State = EntityState.Detached;

                    // Adjuntar el nuevo producto y cambiar su estado a modificado
                    database.Productos.Attach(productos);
                    database.Entry(productos).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    database.SaveChanges();

                    // La operación fue exitosa
                    comprobacion = true;
                }

                // Devolver una respuesta JSON indicando el resultado de la operación
                return new JsonResult(comprobacion);
            }
        }

        // Método HTTP DELETE para eliminar un producto existente
        [HttpDelete]
        public JsonResult ProductosDelete([FromBody] ProductoModel productos)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Buscar el producto existente por su ID
                var existe = database.Productos.SingleOrDefault(p => p.idProducto == productos.idProducto);

                // Verificar si el producto existe
                if (existe != null)
                {
                    // Cambiar el estado de la entidad existente a desvinculado
                    database.Entry(existe).State = EntityState.Detached;

                    // Adjuntar el producto y cambiar su estado a eliminado
                    database.Productos.Attach(productos);
                    database.Entry(productos).State = EntityState.Deleted;

                    // Guardar los cambios en la base de datos
                    database.SaveChanges();

                    // La operación fue exitosa
                    comprobacion = true;
                }

                // Devolver una respuesta JSON indicando el resultado de la operación
                return new JsonResult(comprobacion);
            }
        }
    }
}

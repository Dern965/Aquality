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
    [Route("TiendasController")]
    public class TiendaController : ControllerBase
    {
        // Método HTTP GET para obtener la lista de tiendas
        [HttpGet]
        public JsonResult TiendasGet()
        {
            // Lista para almacenar las tiendas
            List<TiendaModel> tiendas = new List<TiendaModel>();

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Obtener todas las tiendas de la base de datos
                var aux = database.Tiendas;

                // Iterar sobre cada tienda y agregarla a la lista
                foreach (var item in aux)
                {
                    tiendas.Add(new TiendaModel
                    {
                        idTienda = item.idTienda,
                        nombreTienda = item.nombreTienda,
                        ubicacion = item.ubicacion,
                        telefono = item.telefono,
                        idProductos = item.idProductos,
                    });
                }
            }

            // Devolver la lista de tiendas en formato JSON
            return new JsonResult(tiendas);
        }

        // Método HTTP POST para agregar una nueva tienda
        [HttpPost]
        public JsonResult TiendasPost([FromBody] TiendaModel tiendas)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Agregar la nueva tienda a la base de datos
                database.Tiendas.Add(tiendas);

                // Guardar los cambios en la base de datos
                database.SaveChanges();

                // La operación fue exitosa
                comprobacion = true;
            }

            // Devolver una respuesta JSON indicando el resultado de la operación
            return new JsonResult(comprobacion);
        }

        // Método HTTP PATCH para actualizar una tienda existente
        [HttpPatch]
        public JsonResult TiendasUpdate([FromBody] TiendaModel tiendas)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Buscar la tienda existente por su ID
                var existe = database.Tiendas.SingleOrDefault(p => p.idTienda == tiendas.idTienda);

                // Verificar si la tienda existe
                if (existe != null)
                {
                    // Cambiar el estado de la entidad existente a desvinculado
                    database.Entry(existe).State = EntityState.Detached;

                    // Adjuntar la nueva tienda y cambiar su estado a modificado
                    database.Tiendas.Attach(tiendas);
                    database.Entry(tiendas).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    database.SaveChanges();

                    // La operación fue exitosa
                    comprobacion = true;
                }

                // Devolver una respuesta JSON indicando el resultado de la operación
                return new JsonResult(comprobacion);
            }
        }

        // Método HTTP DELETE para eliminar una tienda existente
        [HttpDelete]
        public JsonResult TiendasDelete([FromBody] TiendaModel tiendas)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Buscar la tienda existente por su ID
                var existe = database.Tiendas.SingleOrDefault(p => p.idTienda == tiendas.idTienda);

                // Verificar si la tienda existe
                if (existe != null)
                {
                    // Cambiar el estado de la entidad existente a desvinculado
                    database.Entry(existe).State = EntityState.Detached;

                    // Adjuntar la tienda y cambiar su estado a eliminado
                    database.Tiendas.Attach(tiendas);
                    database.Entry(tiendas).State = EntityState.Deleted;

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

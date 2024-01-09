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
    [Route("CarritosController")]
    public class CarritoController : ControllerBase
    {
        // Método HTTP GET para obtener la lista de carritos
        [HttpGet]
        public JsonResult CarritosGet()
        {
            List<CarritoModel> carritos = new List<CarritoModel>();

            // Uso de un contexto de base de datos para acceder a los datos
            using (AqualityContext database = new AqualityContext())
            {
                // Consulta a la base de datos para obtener todos los carritos
                var aux = database.Carritos;

                // Iteración a través de los resultados y construcción de objetos CarritoModel
                foreach (var item in aux)
                {
                    carritos.Add(new CarritoModel
                    {
                        idCarrito = item.idCarrito,
                        usuario = item.usuario,
                        productos = item.productos,
                    });
                }
            }

            // Devuelve la lista de carritos en formato JSON
            return new JsonResult(carritos);
        }

        // Método HTTP POST para agregar un nuevo carrito
        [HttpPost]
        public JsonResult CarritosPost([FromBody] CarritoModel carritos)
        {
            bool comprobacion = false;

            // Uso de un contexto de base de datos para acceder y modificar datos
            using (AqualityContext database = new AqualityContext())
            {
                // Agrega el nuevo carrito a la base de datos y guarda los cambios
                database.Carritos.Add(carritos);
                database.SaveChanges();
                comprobacion = true;
            }

            // Devuelve una respuesta JSON indicando el resultado de la operación
            return new JsonResult(comprobacion);
        }

        // Método HTTP PATCH para actualizar un carrito existente
        [HttpPatch]
        public JsonResult CarritosUpdate([FromBody] CarritoModel carritos)
        {
            bool comprobacion = false;

            // Uso de un contexto de base de datos para acceder y modificar datos
            using (AqualityContext database = new AqualityContext())
            {
                // Busca el carrito existente por su ID
                var existe = database.Carritos.SingleOrDefault(p => p.idCarrito == carritos.idCarrito);

                if (existe != null)
                {
                    // Desvincula la entidad existente y adjunta la actualizada para su modificación
                    database.Entry(existe).State = EntityState.Detached;
                    database.Carritos.Attach(carritos);
                    database.Entry(carritos).State = EntityState.Modified;
                    // Guarda los cambios en la base de datos
                    database.SaveChanges();
                    comprobacion = true;
                }

                // Devuelve una respuesta JSON indicando el resultado de la operación
                return new JsonResult(comprobacion);
            }
        }

        // Método HTTP DELETE para eliminar un carrito existente
        [HttpDelete]
        public JsonResult CarritosDelete([FromBody] CarritoModel carritos)
        {
            bool comprobacion = false;

            // Uso de un contexto de base de datos para acceder y modificar datos
            using (AqualityContext database = new AqualityContext())
            {
                // Busca el carrito existente por su ID
                var existe = database.Carritos.SingleOrDefault(p => p.idCarrito == carritos.idCarrito);

                if (existe != null)
                {
                    // Desvincula la entidad existente y adjunta la actualizada para su eliminación
                    database.Entry(existe).State = EntityState.Detached;
                    database.Carritos.Attach(carritos);
                    database.Entry(carritos).State = EntityState.Deleted;
                    // Guarda los cambios en la base de datos
                    database.SaveChanges();
                    comprobacion = true;
                }

                // Devuelve una respuesta JSON indicando el resultado de la operación
                return new JsonResult(comprobacion);
            }
        }
    }
}
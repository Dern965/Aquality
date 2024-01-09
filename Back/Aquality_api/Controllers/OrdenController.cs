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
    [Route("OrdenesController")]
    public class OrdenController : ControllerBase
    {
        // Método HTTP GET para obtener la lista de órdenes
        [HttpGet]
        public JsonResult OrdenesGet()
        {
            // Lista para almacenar las órdenes
            List<OrdenModel> ordenes = new List<OrdenModel>();

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Obtener todas las órdenes de la base de datos
                var aux = database.Ordenes;

                // Iterar sobre cada orden y agregarla a la lista
                foreach (var item in aux)
                {
                    ordenes.Add(new OrdenModel
                    {
                        idOrden = item.idOrden,
                        idCarrito = item.idCarrito,
                        fechaEntrega = item.fechaEntrega,
                        seguimiento = item.seguimiento
                    });
                }
            }

            // Devolver la lista de órdenes en formato JSON
            return new JsonResult(ordenes);
        }

        // Método HTTP POST para agregar una nueva orden
        [HttpPost]
        public JsonResult OrdenesPost([FromBody] OrdenModel ordenes)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Agregar la nueva orden a la base de datos
                database.Ordenes.Add(ordenes);

                // Guardar los cambios en la base de datos
                database.SaveChanges();

                // La operación fue exitosa
                comprobacion = true;
            }

            // Devolver una respuesta JSON indicando el resultado de la operación
            return new JsonResult(comprobacion);
        }

        // Método HTTP PATCH para actualizar una orden existente
        [HttpPatch]
        public JsonResult OrdenesUpdate([FromBody] OrdenModel ordenes)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Buscar la orden existente por su ID
                var existe = database.Ordenes.SingleOrDefault(p => p.idOrden == ordenes.idOrden);

                // Verificar si la orden existe
                if (existe != null)
                {
                    // Cambiar el estado de la entidad existente a desvinculado
                    database.Entry(existe).State = EntityState.Detached;

                    // Adjuntar la nueva orden y cambiar su estado a modificado
                    database.Ordenes.Attach(ordenes);
                    database.Entry(ordenes).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    database.SaveChanges();

                    // La operación fue exitosa
                    comprobacion = true;
                }

                // Devolver una respuesta JSON indicando el resultado de la operación
                return new JsonResult(comprobacion);
            }
        }

        // Método HTTP DELETE para eliminar una orden existente
        [HttpDelete]
        public JsonResult OrdenesDelete([FromBody] OrdenModel ordenes)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Buscar la orden existente por su ID
                var existe = database.Ordenes.SingleOrDefault(p => p.idOrden == ordenes.idOrden);

                // Verificar si la orden existe
                if (existe != null)
                {
                    // Cambiar el estado de la entidad existente a desvinculado
                    database.Entry(existe).State = EntityState.Detached;

                    // Adjuntar la orden y cambiar su estado a eliminado
                    database.Ordenes.Attach(ordenes);
                    database.Entry(ordenes).State = EntityState.Deleted;

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

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
    [Route("HistorialesController")]
    public class HistorialController : ControllerBase
    {
        // Método HTTP GET para obtener la lista de historiales
        [HttpGet]
        public JsonResult HistorialesGet()
        {
            // Lista para almacenar los historiales
            List<HistorialModel> historiales = new List<HistorialModel>();

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Obtener todos los historiales de la base de datos
                var aux = database.Historiales;

                // Iterar sobre cada historial y agregarlo a la lista
                foreach (var item in aux)
                {
                    historiales.Add(new HistorialModel
                    {
                        idHistorial = item.idHistorial,
                        idUsuarios = item.idUsuarios,
                        idOrdenes = item.idOrdenes,
                    });
                }
            }

            // Devolver la lista de historiales en formato JSON
            return new JsonResult(historiales);
        }

        // Método HTTP POST para agregar un nuevo historial
        [HttpPost]
        public JsonResult HistorialesPost([FromBody] HistorialModel historiales)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Agregar el nuevo historial a la base de datos
                database.Historiales.Add(historiales);

                // Guardar los cambios en la base de datos
                database.SaveChanges();

                // La operación fue exitosa
                comprobacion = true;
            }

            // Devolver una respuesta JSON indicando el resultado de la operación
            return new JsonResult(comprobacion);
        }

        // Método HTTP PATCH para actualizar un historial existente
        [HttpPatch]
        public JsonResult HistorialesUpdate([FromBody] HistorialModel historiales)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Buscar el historial existente por su ID
                var existe = database.Historiales.SingleOrDefault(p => p.idHistorial == historiales.idHistorial);

                // Verificar si el historial existe
                if (existe != null)
                {
                    // Cambiar el estado de la entidad existente a desvinculado
                    database.Entry(existe).State = EntityState.Detached;

                    // Adjuntar el nuevo historial y cambiar su estado a modificado
                    database.Historiales.Attach(historiales);
                    database.Entry(historiales).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    database.SaveChanges();

                    // La operación fue exitosa
                    comprobacion = true;
                }

                // Devolver una respuesta JSON indicando el resultado de la operación
                return new JsonResult(comprobacion);
            }
        }

        // Método HTTP DELETE para eliminar un historial existente
        [HttpDelete]
        public JsonResult HistorialDelete([FromBody] HistorialModel historiales)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Buscar el historial existente por su ID
                var existe = database.Historiales.SingleOrDefault(p => p.idHistorial == historiales.idHistorial);

                // Verificar si el historial existe
                if (existe != null)
                {
                    // Cambiar el estado de la entidad existente a desvinculado
                    database.Entry(existe).State = EntityState.Detached;

                    // Adjuntar el historial y cambiar su estado a eliminado
                    database.Historiales.Attach(historiales);
                    database.Entry(historiales).State = EntityState.Deleted;

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

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
    [Route("EspeciesController")]
    public class EspecieController : ControllerBase
    {
        // Método HTTP GET para obtener la lista de especies
        [HttpGet]
        public JsonResult EspeciesGet()
        {
            // Lista para almacenar las especies
            List<EspecieModel> especies = new List<EspecieModel>();

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Obtener todas las especies de la base de datos
                var aux = database.Especies;

                // Iterar sobre cada especie y agregarla a la lista
                foreach (var item in aux)
                {
                    especies.Add(new EspecieModel
                    {
                        especie = item.especie,
                        habitat = item.habitat,
                        conservacion = item.conservacion,
                        adaptabilidad = item.adaptabilidad,
                        amenazas = item.amenazas,
                        contaminacion = item.contaminacion,
                        consejos = item.consejos,
                        poblacion = item.poblacion,
                        idProductos = item.idProductos
                    });
                }
            }

            // Devolver la lista de especies en formato JSON
            return new JsonResult(especies);
        }

        // Método HTTP POST para agregar una nueva especie
        [HttpPost]
        public JsonResult EspeciesPost([FromBody] EspecieModel especies)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Agregar la nueva especie a la base de datos
                database.Especies.Add(especies);

                // Guardar los cambios en la base de datos
                database.SaveChanges();

                // La operación fue exitosa
                comprobacion = true;
            }

            // Devolver una respuesta JSON indicando el resultado de la operación
            return new JsonResult(comprobacion);
        }

        // Método HTTP PATCH para actualizar una especie existente
        [HttpPatch]
        public JsonResult EspeciesUpdate([FromBody] EspecieModel especies)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Buscar la especie existente por su nombre
                var existe = database.Especies.SingleOrDefault(p => p.especie == especies.especie);

                // Verificar si la especie existe
                if (existe != null)
                {
                    // Cambiar el estado de la entidad existente a desvinculado
                    database.Entry(existe).State = EntityState.Detached;

                    // Adjuntar la nueva especie y cambiar su estado a modificado
                    database.Especies.Attach(especies);
                    database.Entry(especies).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    database.SaveChanges();

                    // La operación fue exitosa
                    comprobacion = true;
                }

                // Devolver una respuesta JSON indicando el resultado de la operación
                return new JsonResult(comprobacion);
            }
        }

        // Método HTTP DELETE para eliminar una especie existente
        [HttpDelete]
        public JsonResult EspeciesDelete([FromBody] EspecieModel especies)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Buscar la especie existente por su nombre
                var existe = database.Especies.SingleOrDefault(p => p.especie == especies.especie);

                // Verificar si la especie existe
                if (existe != null)
                {
                    // Cambiar el estado de la entidad existente a desvinculado
                    database.Entry(existe).State = EntityState.Detached;

                    // Adjuntar la especie y cambiar su estado a eliminado
                    database.Especies.Attach(especies);
                    database.Entry(especies).State = EntityState.Deleted;

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

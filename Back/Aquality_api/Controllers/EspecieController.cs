using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aquality_api.Context;
using Aquality_api.Model;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aquality_api.Controllers
{
    [ApiController]
    [Route("EspeciesController")]
    public class EspecieController : ControllerBase
    {
        [HttpGet]
        public JsonResult EspeciesGet()
        {
            List<EspecieModel> especies = new List<EspecieModel>();
            using (AqualityContext database = new AqualityContext())
            {
                var aux = database.Especies;
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
            return new JsonResult(especies);
        }

        [HttpPost]
        public JsonResult EspeciesPost([FromBody] EspecieModel especies)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                database.Especies.Add(especies);
                database.SaveChanges();
                comprobacion = true;
            }
            return new JsonResult(comprobacion);
        }

        [HttpPatch]
        public JsonResult EspeciesUpdate([FromBody] EspecieModel especies)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Especies.SingleOrDefault(p => p.especie == especies.especie);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Especies.Attach(especies);
                    database.Entry(especies).State = EntityState.Modified;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }

        [HttpDelete]
        public JsonResult EspeciesDelete([FromBody] EspecieModel especies)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Especies.SingleOrDefault(p => p.especie == especies.especie);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Especies.Attach(especies);
                    database.Entry(especies).State = EntityState.Deleted;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }
    }
}

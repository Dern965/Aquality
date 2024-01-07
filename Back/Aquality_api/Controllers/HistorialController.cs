using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aquality_api.Context;
using Aquality_api.Model;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aquality_api.Controllers
{
    [ApiController]
    [Route("HistorialesController")]
    public class HistorialController : ControllerBase
    {
        [HttpGet]
        public JsonResult HistorialesGet()
        {
            List<HistorialModel> historiales = new List<HistorialModel>();
            using (AqualityContext database = new AqualityContext())
            {
                var aux = database.Historiales;
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
            return new JsonResult(historiales);
        }

        [HttpPost]
        public JsonResult HistorialesPost([FromBody] HistorialModel historiales)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                database.Historiales.Add(historiales);
                database.SaveChanges();
                comprobacion = true;
            }
            return new JsonResult(comprobacion);
        }

        [HttpPatch]
        public JsonResult HistorialesUpdate([FromBody] HistorialModel historiales)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Historiales.SingleOrDefault(p => p.idHistorial == historiales.idHistorial);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Historiales.Attach(historiales);
                    database.Entry(historiales).State = EntityState.Modified;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }

        [HttpDelete]
        public JsonResult HistorialDelete([FromBody] HistorialModel historiales)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Historiales.SingleOrDefault(p => p.idHistorial == historiales.idHistorial);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Historiales.Attach(historiales);
                    database.Entry(historiales).State = EntityState.Deleted;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }
    }
}

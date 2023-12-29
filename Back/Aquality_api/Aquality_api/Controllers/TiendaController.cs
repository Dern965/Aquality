using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aquality_api.Context;
using Aquality_api.Model;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aquality_api.Controllers
{
    [ApiController]
    [Route("Tiendas")]
    public class TiendaController : ControllerBase
    {
        [HttpGet]
        public JsonResult TiendasGet()
        {
            List<TiendaModel> tiendas = new List<TiendaModel>();
            using (AqualityContext database = new AqualityContext())
            {
                var aux = database.Tiendas;
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
            return new JsonResult(tiendas);
        }

        [HttpPost]
        public JsonResult TiendasPost([FromBody] TiendaModel tiendas)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                database.Tiendas.Add(tiendas);
                database.SaveChanges();
                comprobacion = true;
            }
            return new JsonResult(comprobacion);
        }

        [HttpPatch]
        public JsonResult TiendasUpdate([FromBody] TiendaModel tiendas)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Tiendas.SingleOrDefault(p => p.idTienda == tiendas.idTienda);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Tiendas.Attach(tiendas);
                    database.Entry(tiendas).State = EntityState.Modified;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }

        [HttpDelete]
        public JsonResult TiendasDelete([FromBody] TiendaModel tiendas)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Tiendas.SingleOrDefault(p => p.idTienda == tiendas.idTienda);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Tiendas.Attach(tiendas);
                    database.Entry(tiendas).State = EntityState.Deleted;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }
    }
}

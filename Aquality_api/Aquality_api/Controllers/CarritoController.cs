using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aquality_api.Context;
using Aquality_api.Model;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aquality_api.Controllers
{
    [ApiController]
    [Route("Carritos")]
    public class CarritoController : ControllerBase
    {
        [HttpGet]
        public JsonResult CarritosGet()
        {
            List<CarritoModel> carritos = new List<CarritoModel>();
            using (AqualityContext database = new AqualityContext())
            {
                var aux = database.Carritos;
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
            return new JsonResult(carritos);
        }

        [HttpPost]
        public JsonResult CarritosPost([FromBody] CarritoModel carritos)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                database.Carritos.Add(carritos);
                database.SaveChanges();
                comprobacion = true;
            }
            return new JsonResult(comprobacion);
        }

        [HttpPatch]
        public JsonResult CarritosUpdate([FromBody] CarritoModel carritos)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Carritos.SingleOrDefault(p => p.idCarrito == carritos.idCarrito);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Carritos.Attach(carritos);
                    database.Entry(carritos).State = EntityState.Modified;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }

        [HttpDelete]
        public JsonResult CarritosDelete([FromBody] CarritoModel carritos)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Carritos.SingleOrDefault(p => p.idCarrito == carritos.idCarrito);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Carritos.Attach(carritos);
                    database.Entry(carritos).State = EntityState.Deleted;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }
    }
}

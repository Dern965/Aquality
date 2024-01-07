using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aquality_api.Context;
using Aquality_api.Model;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aquality_api.Controllers
{
    [ApiController]
    [Route("OrdenesController")]
    public class OrdenController : ControllerBase
    {
        [HttpGet]
        public JsonResult OrdenesGet()
        {
            List<OrdenModel> ordenes = new List<OrdenModel>();
            using (AqualityContext database = new AqualityContext())
            {
                var aux = database.Ordenes;
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
            return new JsonResult(ordenes);
        }

        [HttpPost]
        public JsonResult OrdenesPost([FromBody] OrdenModel ordenes)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                database.Ordenes.Add(ordenes);
                database.SaveChanges();
                comprobacion = true;
            }
            return new JsonResult(comprobacion);
        }

        [HttpPatch]
        public JsonResult OrdenesUpdate([FromBody] OrdenModel ordenes)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Ordenes.SingleOrDefault(p => p.idOrden == ordenes.idOrden);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Ordenes.Attach(ordenes);
                    database.Entry(ordenes).State = EntityState.Modified;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }

        [HttpDelete]
        public JsonResult OrdenesDelete([FromBody] OrdenModel ordenes)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Ordenes.SingleOrDefault(p => p.idOrden == ordenes.idOrden);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Ordenes.Attach(ordenes);
                    database.Entry(ordenes).State = EntityState.Deleted;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }
    }
}
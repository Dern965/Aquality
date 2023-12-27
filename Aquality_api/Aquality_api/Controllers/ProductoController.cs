using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aquality_api.Context;
using Aquality_api.Model;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aquality_api.Controllers
{
    [ApiController]
    [Route("Productos")]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        public JsonResult ProductosGet()
        {
            List<ProductoModel> productos = new List<ProductoModel>();
            using (AqualityContext database = new AqualityContext())
            {
                var aux = database.Productos;
                foreach (var item in aux)
                {
                    productos.Add(new ProductoModel
                    {
                        idProducto = item.idProducto,
                        nombreProducto = item.nombreProducto,
                        materiales = item.materiales,
                        fabricacion = item.fabricacion,
                        precio = item.precio,
                        disponibilidad = item.disponibilidad,
                        idTienda = item.idTienda,
                    });
                }
            }
            return new JsonResult(productos);
        }

        [HttpPost]
        public JsonResult ProductosPost([FromBody] ProductoModel productos)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                database.Productos.Add(productos);
                database.SaveChanges();
                comprobacion = true;
            }
            return new JsonResult(comprobacion);
        }

        [HttpPatch]
        public JsonResult ProductosUpdate([FromBody] ProductoModel productos)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Productos.SingleOrDefault(p => p.idProducto == productos.idProducto);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Productos.Attach(productos);
                    database.Entry(productos).State = EntityState.Modified;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }

        [HttpDelete]
        public JsonResult ProductosDelete([FromBody] ProductoModel productos)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var existe = database.Productos.SingleOrDefault(p => p.idProducto == productos.idProducto);
                if (existe != null)
                {
                    database.Entry(existe).State = EntityState.Detached;
                    database.Productos.Attach(productos);
                    database.Entry(productos).State = EntityState.Deleted;
                    database.SaveChanges();
                    comprobacion = true;
                }
                return new JsonResult(comprobacion);
            }
        }
    }
}

using Aquality_api.Context;
using Aquality_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography.X509Certificates;

namespace Aquality_api.Controllers
{
    [Route("UsuariosController")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetUsers()
        {
            List<UsuarioModel> users = new List<UsuarioModel>();
            using (AqualityContext database = new AqualityContext())
            {
                var aux = database.Usuarios;
                foreach(var item in aux)
                {
                    users.Add(new UsuarioModel
                    {
                        username = item.username,
                        email = item.email,
                        password = item.password,
                        phone = item.phone,
                        first_Name = item.first_Name,
                        last_Name = item.last_Name,
                        birth_Day = item.birth_Day,
                        birth_Month = item.birth_Month,
                        birth_Year = item.birth_Year,
                        idHistorial = item.idHistorial
                    });
                }
            }
            return new JsonResult(users);
        }

        [HttpPost]
        public JsonResult PostUser([FromBody]UsuarioModel user)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var aux = database.Usuarios;
                foreach(var item in aux)
                {
                    if (item.username == user.username)
                    {
                        comprobacion = true;
                    }
                }
                if (comprobacion == false)
                {
                    database.Usuarios.Add(user);
                    database.SaveChanges();
                    return new JsonResult("Usuario creado");
                }
                else
                {
                    return new JsonResult("Usuario ya existente");
                }
            }
        }
        [HttpPatch]
        public JsonResult UpdateUser([FromBody]UsuarioModel user)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var aux = database.Usuarios;
                foreach (var item in aux)
                {
                    if (item.username == user.username)
                    {
                        comprobacion = true;
                    }
                }
                if (comprobacion == true)
                {
                    var existe = database.Usuarios.SingleOrDefault(u => u.username == user.username);
                    if (existe != null)
                    {
                        database.Entry(existe).State = EntityState.Detached;
                        database.Usuarios.Attach(user);
                        database.Entry(user).State = EntityState.Modified;
                        database.SaveChanges();
                    }
                    return new JsonResult("Usuario actualizado");
                }
                else
                {
                    return new JsonResult("Usuario no existente");
                }
            }
        }
        [HttpDelete]
        public JsonResult DeleteUser([FromBody]UsuarioModel user)
        {
            bool comprobacion = false;
            using (AqualityContext database = new AqualityContext())
            {
                var aux = database.Usuarios;
                foreach (var item in aux)
                {
                    if (item.username == user.username)
                    {
                        comprobacion = true;
                    }
                }
                if (comprobacion == true)
                {
                    var existe = database.Usuarios.SingleOrDefault(u => u.username == user.username);
                    if (existe != null)
                    {
                        database.Entry(existe).State = EntityState.Detached;
                        database.Usuarios.Attach(user);
                        database.Entry(user).State = EntityState.Deleted;
                        database.SaveChanges();
                    }
                    return new JsonResult("Usuario eliminado");
                }
                else
                {
                    return new JsonResult("Usuario no existente");
                }
            }
        }
    }
}

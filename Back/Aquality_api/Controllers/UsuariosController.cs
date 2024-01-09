using Aquality_api.Context;
using Aquality_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography.X509Certificates;

namespace Aquality_api.Controllers
{
    // Especificación de la ruta base para este controlador
    [Route("UsuariosController")]
    // Especificación de que es un controlador de API
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        // Método HTTP GET para obtener la lista de usuarios
        [HttpGet]
        public JsonResult GetUsers()
        {
            // Lista para almacenar los usuarios
            List<UsuarioModel> users = new List<UsuarioModel>();

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Obtener todos los usuarios de la base de datos
                var aux = database.Usuarios;

                // Iterar sobre cada usuario y agregarlo a la lista
                foreach (var item in aux)
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

            // Devolver la lista de usuarios en formato JSON
            return new JsonResult(users);
        }

        // Método HTTP POST para agregar un nuevo usuario
        [HttpPost]
        public JsonResult PostUser([FromBody] UsuarioModel user)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Obtener todos los usuarios de la base de datos
                var aux = database.Usuarios;

                // Iterar sobre cada usuario para comprobar si ya existe el mismo nombre de usuario
                foreach (var item in aux)
                {
                    if (item.username == user.username)
                    {
                        comprobacion = true;
                    }
                }

                // Verificar si el usuario no existe
                if (comprobacion == false)
                {
                    // Agregar el nuevo usuario a la base de datos
                    database.Usuarios.Add(user);

                    // Guardar los cambios en la base de datos
                    database.SaveChanges();

                    // Devolver una respuesta JSON indicando el éxito de la operación
                    return new JsonResult("Usuario creado");
                }
                else
                {
                    // Devolver una respuesta JSON indicando que el usuario ya existe
                    return new JsonResult("Usuario ya existente");
                }
            }
        }

        // Método HTTP PATCH para actualizar un usuario existente
        [HttpPatch]
        public JsonResult UpdateUser([FromBody] UsuarioModel user)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Obtener todos los usuarios de la base de datos
                var aux = database.Usuarios;

                // Iterar sobre cada usuario para comprobar si existe el mismo nombre de usuario
                foreach (var item in aux)
                {
                    if (item.username == user.username)
                    {
                        comprobacion = true;
                    }
                }

                // Verificar si el usuario existe
                if (comprobacion == true)
                {
                    // Buscar el usuario existente por su nombre de usuario
                    var existe = database.Usuarios.SingleOrDefault(u => u.username == user.username);

                    // Verificar si el usuario existe
                    if (existe != null)
                    {
                        // Cambiar el estado de la entidad existente a desvinculado
                        database.Entry(existe).State = EntityState.Detached;

                        // Adjuntar el usuario actualizado y cambiar su estado a modificado
                        database.Usuarios.Attach(user);
                        database.Entry(user).State = EntityState.Modified;

                        // Guardar los cambios en la base de datos
                        database.SaveChanges();
                    }

                    // Devolver una respuesta JSON indicando el éxito de la operación
                    return new JsonResult("Usuario actualizado");
                }
                else
                {
                    // Devolver una respuesta JSON indicando que el usuario no existe
                    return new JsonResult("Usuario no existente");
                }
            }
        }

        // Método HTTP DELETE para eliminar un usuario existente
        [HttpDelete]
        public JsonResult DeleteUser([FromBody] UsuarioModel user)
        {
            // Variable para verificar el éxito de la operación
            bool comprobacion = false;

            // Crear un contexto de base de datos
            using (AqualityContext database = new AqualityContext())
            {
                // Obtener todos los usuarios de la base de datos
                var aux = database.Usuarios;

                // Iterar sobre cada usuario para comprobar si existe el mismo nombre de usuario
                foreach (var item in aux)
                {
                    if (item.username == user.username)
                    {
                        comprobacion = true;
                    }
                }

                // Verificar si el usuario existe
                if (comprobacion == true)
                {
                    // Buscar el usuario existente por su nombre de usuario
                    var existe = database.Usuarios.SingleOrDefault(u => u.username == user.username);

                    // Verificar si el usuario existe
                    if (existe != null)
                    {
                        // Cambiar el estado de la entidad existente a desvinculado
                        database.Entry(existe).State = EntityState.Detached;

                        // Adjuntar el usuario a eliminar y cambiar su estado a eliminado
                        database.Usuarios.Attach(user);
                        database.Entry(user).State = EntityState.Deleted;

                        // Guardar los cambios en la base de datos
                        database.SaveChanges();
                    }

                    // Devolver una respuesta JSON indicando el éxito de la operación
                    return new JsonResult("Usuario eliminado");
                }
                else
                {
                    // Devolver una respuesta JSON indicando que el usuario no existe
                    return new JsonResult("Usuario no existente");
                }
            }
        }
    }
}

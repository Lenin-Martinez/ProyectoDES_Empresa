using ProyectoDES_Empresa.Controllers;
using ProyectoDES_Empresa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProyectoDES_Empresa.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GestionInventarios.Tests
{
    public class UsuariosControllerTests
    {
        //Registrar un usuario Valido
        [Fact]
        public async Task Post_Registrar_Usuario_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new UsuariosController(context);
            var nuevoUsuarioVM = new UsuarioVM
            {
                CorreoUsuario = "nuevo@correo.com",
                ClaveUsuario = "Clave123",
                ConfirmarClaveUsuario = "Clave123"
            };

            var result = await controller.Registrar(nuevoUsuarioVM);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Registro Exitoso!", controller.ViewBag.RegistroExitoso);

            var usuarioEnDb = context.Usuarios.FirstOrDefault(u => u.CorreoUsuario == "nuevo@correo.com");
            Assert.NotNull(usuarioEnDb);
            Assert.True(BCrypt.Net.BCrypt.Verify("Clave123", usuarioEnDb.ClaveUsuario));
        }

        //No se registra un usuario cuando las contraseñas no coinciden
        [Fact]
        public async Task Post_Registrar_Usuario_CuandoClavesNoCoinciden()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new UsuariosController(context);
            var nuevoUsuarioVM = new UsuarioVM
            {
                CorreoUsuario = "nuevo@correo.com",
                ClaveUsuario = "Clave123",
                ConfirmarClaveUsuario = "NoClave123"
            };

            var result = await controller.Registrar(nuevoUsuarioVM);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error al registar usuario: Las contraseñas no coinciden", controller.ViewBag.ErrorClave);
        }

        //No se registra porque el correo ya esta registrado en la BD
        [Fact]
        public async Task Post_Registrar_Usuario_CuandoCorreoYaRegistrado()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new UsuariosController(context);
            var usuarioExistente = new Usuario
            {
                CorreoUsuario = "nuevo@correo.com",
                ClaveUsuario = BCrypt.Net.BCrypt.HashPassword("Clave123"),
                IdRol = 2
            };
            context.Usuarios.Add(usuarioExistente);
            await context.SaveChangesAsync();

            var nuevoUsuarioVM = new UsuarioVM
            {
                CorreoUsuario = "nuevo@correo.com",
                ClaveUsuario = "Clave123",
                ConfirmarClaveUsuario = "Clave123"
            };

            var result = await controller.Registrar(nuevoUsuarioVM);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error: El correo brindado ya posee cuenta registrada.", controller.ViewBag.ErrorClave);
        }

        //No inicia sesion si el usuario no existe en la base de datos
        [Fact]
        public async Task Login_CuandoUsuarioNoExiste()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new UsuariosController(context);

            var loginVM = new LoginVM
            {
                CorreoUsuario = "noregistrado@correo.com",
                ClaveUsuario = "Clave123"
            };

            var result = await controller.Login(loginVM);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error: Las credenciales son incorrectas", controller.ViewBag.MensajeError);
        }

        //No inicia sesion cuando la contraseña es incorrecta
        [Fact]
        public async Task Login_CuandoClaveEsIncorrecta()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new UsuariosController(context);
            var usuario = new Usuario
            {
                CorreoUsuario = "nuevo@correo.com",
                ClaveUsuario = BCrypt.Net.BCrypt.HashPassword("Clave123"),
                IdRol = 2,
            };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            //Verifica credenciales
            var loginVM = new LoginVM
            {
                CorreoUsuario = "nuevo@correo.com",
                ClaveUsuario = "NoClave123"
            };

            var result = await controller.Login(loginVM);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error: Las credenciales son incorrectas", controller.ViewBag.MensajeError);
        }

        //Redirige correctamente a vista editar cuando el Id es valido
        [Fact]
        public async Task Editar_RetornaVistaConUsuario_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new UsuariosController(context);

            var usuario = new Usuario
            {
                CorreoUsuario = "nuevo@correo.com",
                ClaveUsuario = BCrypt.Net.BCrypt.HashPassword("Clave123"),
                IdRol = 2,
            };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var result = await controller.Editar(usuario.ID);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<EditarUsuarioVM>(viewResult.Model);
            Assert.Equal(usuario.CorreoUsuario, model.CorreoUsuario);
        }

        //No retorna a vista editar puesto que el Id es invalido
        [Fact]
        public async Task Editar_RetornaNotFound_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new UsuariosController(context);

            var result = await controller.Editar(999); 

            Assert.IsType<NotFoundResult>(result);
        }

        //Edita la informacion del usuario correctamente cuando el modelo es valido
        [Fact]
        public async Task Editar_Usuario_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new UsuariosController(context);

            //Crea un nuevo usuario
            var usuario = new Usuario
            {
                CorreoUsuario = "nuevo@correo.com",
                ClaveUsuario = BCrypt.Net.BCrypt.HashPassword("Clave123"),
                IdRol = 2,
            }; 
            
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            //Nueva informacion del usuario
            var editarUsuario = new EditarUsuarioVM
            {
                ID = usuario.ID,
                CorreoUsuario = "nuevo@correo.com",
                ClaveAntigua = "Clave123",
                NuevaClaveUsuario = "NuevaClave123",
                ConfirmarNuevaClaveUsuario = "NuevaClave123"
            };

            var result = await controller.Editar(editarUsuario);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectResult.ActionName);

            var usuarioEnDb = context.Usuarios.FirstOrDefault(p => p.ID == usuario.ID);
            Assert.NotNull(usuarioEnDb);
            Assert.Equal("nuevo@correo.com", usuarioEnDb.CorreoUsuario);
            Assert.True(BCrypt.Net.BCrypt.Verify("NuevaClave123", usuarioEnDb.ClaveUsuario));
        }

        //No actualiza el resgitro puesto que la informacion no es valida
        [Fact]
        public async Task Editar_Post_Usuario_CuandoNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new UsuariosController(context);

            //Crea un nuevo usuario
            var usuario = new Usuario
            {
                CorreoUsuario = "nuevo@correo.com",
                ClaveUsuario = BCrypt.Net.BCrypt.HashPassword("Clave123"),
                IdRol = 2,
            };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            //Edita el usuario previamente creado pero las claves las nuevas claves no coinciden
            var editarUsuario = new EditarUsuarioVM
            {
                ID = usuario.ID,
                CorreoUsuario = "nuevo@correo.com",
                ClaveAntigua = "Clave123",
                NuevaClaveUsuario = "NuevaClave123",
                ConfirmarNuevaClaveUsuario = "NoNuevaClave123"
            };

            // Se genera un error en el modelo: las claves no coinciden
            controller.ModelState.AddModelError("ConfirmarNuevaClaveUsuario", "Las nuevas contraseñas no coinciden.");

            var result = await controller.Editar(editarUsuario);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<EditarUsuarioVM>(viewResult.Model);
            Assert.Equal(editarUsuario.CorreoUsuario, returnValue.CorreoUsuario);

            // Verificar que el resgitro no se ha actualizado
            var usuarioEnDb = context.Usuarios.FirstOrDefault(p => p.ID == usuario.ID);
            Assert.NotNull(usuarioEnDb);
            Assert.Equal("nuevo@correo.com", usuarioEnDb.CorreoUsuario);
            Assert.True(BCrypt.Net.BCrypt.Verify("Clave123", usuarioEnDb.ClaveUsuario));
        }

        //Elimina un usuario cuando es valido
        [Fact]
        public async Task Eliminar_Usuario_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new UsuariosController(context);

            // Agregar un usuario
            var usuario = new Usuario
            {
                CorreoUsuario = "user@correo.com",
                ClaveUsuario = BCrypt.Net.BCrypt.HashPassword("Clave123"),
                IdRol = 2,
            };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var result = await controller.EliminarConfirmado(usuario.ID);

            //Verifica que el usuario ya no esta en la base de datos.
            var usuarioEliminado = await context.Usuarios.FindAsync(usuario.ID);
            Assert.Null(usuarioEliminado);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }



    }
}

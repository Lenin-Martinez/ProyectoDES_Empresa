using Microsoft.AspNetCore.Mvc;
using ProyectoDES_Empresa.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoDES_Empresa.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace ProyectoDES_Empresa.Controllers
{
        
    public class UsuariosController : Controller
    {
        private readonly EmpresaDBContext _context;

        public UsuariosController(EmpresaDBContext context)
        {
            _context = context;
        }

        [Authorize]

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioVM usuariovm)
        {
            Usuario? usuario_encontrado = await _context.Usuarios
            .Where(u => u.CorreoUsuario == usuariovm.CorreoUsuario)
                .FirstOrDefaultAsync();

            if (usuario_encontrado == null)
            {
                if (usuariovm.ClaveUsuario != usuariovm.ConfirmarClaveUsuario)
                {
                    ViewData["Mensaje"] = "Las contraseñas no coinciden";
                }
                else
                {

                    //Si coinciden se creara un nuevo usuario
                    Usuario usuario = new Usuario()
                    {
                        CorreoUsuario = usuariovm.CorreoUsuario,
                        ClaveUsuario = usuariovm.ClaveUsuario
                    };

                    await _context.Usuarios.AddAsync(usuario);
                    await _context.SaveChangesAsync();


                    if (usuario.ID != 0)
                    {
                        ViewData["Mensaje"] = "Ingreso Exitoso";
                    }
                    else
                    {
                        ViewData["Mensaje"] = "Error al registar usuario";
                    }

                }
                return View();
            }
            else
            {
                ViewData["Mensaje"] = "El correo brindado ya posee cuenta registrada";
            }

            return View();
            
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginvm)
        {
            Usuario? usuario_encontrado = await _context.Usuarios
                .Where(u => u.CorreoUsuario == loginvm.CorreoUsuario && u.ClaveUsuario == loginvm.ClaveUsuario)
                .FirstOrDefaultAsync();

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "Error. El usuario o contraseña es incorrecto";
                return View();
            }
            else
            {
                //AUTENTICACION

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, usuario_encontrado.CorreoUsuario)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    properties
                    );
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

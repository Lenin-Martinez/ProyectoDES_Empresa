using Microsoft.AspNetCore.Mvc;
using ProyectoDES_Empresa.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoDES_Empresa.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BCrypt.Net;
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


        //Regitrar usuarios.
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        //Registrar usuario POST
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioVM usuariovm)
        {
            if (ModelState.IsValid)
            {
                Usuario? usuario_encontrado = await _context.Usuarios
            .Where(u => u.CorreoUsuario == usuariovm.CorreoUsuario)
                .FirstOrDefaultAsync();

                if (usuario_encontrado == null)
                {
                    if (usuariovm.ClaveUsuario != usuariovm.ConfirmarClaveUsuario)
                    {
                        ViewBag.ErrorClave = "Error al registar usuario: Las contraseñas no coinciden";
                    }
                    else
                    {
                        //Hashea la contraseña antes de guardarla en la BD
                        string ClaveEncriptada = BCrypt.Net.BCrypt.HashPassword(usuariovm.ClaveUsuario);

                        //Si coinciden se creara un nuevo usuario
                        Usuario usuario = new Usuario()
                        {
                            CorreoUsuario = usuariovm.CorreoUsuario,
                            ClaveUsuario = ClaveEncriptada,
                            IdRol = 2

                        };

                        await _context.Usuarios.AddAsync(usuario);
                        await _context.SaveChangesAsync();


                        if (usuario.ID != 0)
                        {
                            ViewBag.RegistroExitoso = "Registro Exitoso!";
                        }
                        else
                        {
                            ViewBag.ErrorClave = "Error al registar usuario";
                        }

                    }
                    return View();
                }
                else
                {
                    ViewBag.ErrorClave = "Error: El correo brindado ya posee cuenta registrada.";
                }

                return View();
            }
            else
            {
                return View();
            }
            

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
                .Include(u => u.Rol)
                .Where(u => u.CorreoUsuario == loginvm.CorreoUsuario)
                .FirstOrDefaultAsync();

            if (usuario_encontrado == null || !BCrypt.Net.BCrypt.Verify(loginvm.ClaveUsuario, usuario_encontrado.ClaveUsuario))
            {
                ViewBag.MensajeError = "Error: Las credenciales son incorrectas";
                return View();
            }
            else
            {
                //AUTENTICACION

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, usuario_encontrado.CorreoUsuario),
                    new Claim(ClaimTypes.Role, usuario_encontrado.Rol.NombreRol)

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

        //CRUD PARA USUARIOS
        // Listar usuarios excluyendo Admin
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index(string textoABuscar)
        {
            var usuarios = from p in _context.Usuarios
                            select p;

            if (!String.IsNullOrEmpty(textoABuscar))
            {
                usuarios = usuarios.Where(p => p.CorreoUsuario.Contains(textoABuscar));
            }

            usuarios = usuarios.Where(u => u.IdRol != 1);

            return View(await usuarios.ToListAsync());
        }

        // Editar usuarios excluyendo Admin
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var model = new EditarUsuarioVM
            {
                ID = usuario.ID,
                CorreoUsuario = usuario.CorreoUsuario
            };

            return View(model);
        }

        //Editar usuarios metodo POST
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Editar(EditarUsuarioVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _context.Usuarios.FindAsync(model.ID);
            if (usuario == null)
            {
                return NotFound();
            }

            // Verifica si el correo ya está registrado por otro usuario no permite ingresarlo
            var usuarioConMismoCorreo = await _context.Usuarios
                .Where(u => u.CorreoUsuario == model.CorreoUsuario && u.ID != model.ID)
                .FirstOrDefaultAsync();

            if (usuarioConMismoCorreo != null)
            {
                ViewBag.Error = "Error: El correo brindado ya posee cuenta registrada.";
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.NuevaClaveUsuario) && model.NuevaClaveUsuario == model.ConfirmarNuevaClaveUsuario)
            {
                // Verificar la contraseña antigua
                if (BCrypt.Net.BCrypt.Verify(model.ClaveAntigua, usuario.ClaveUsuario))
                {
                    // Hashear la nueva contraseña
                    usuario.ClaveUsuario = BCrypt.Net.BCrypt.HashPassword(model.NuevaClaveUsuario);
                }
                else
                {
                    ViewBag.Error = "Error: La contraseña antigua es incorrecta.";
                    return View(model);
                }
            }
            else if (!string.IsNullOrEmpty(model.NuevaClaveUsuario))
            {
                ViewBag.Error = "Error: Las nuevas contraseñas no coinciden.";
                return View(model);
            }

            usuario.CorreoUsuario = model.CorreoUsuario;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(model);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }


        // Eliminar usuarios excluyendo Admin
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.ID == id && u.IdRol != 1)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.ID == id && u.IdRol != 1)
                .FirstOrDefaultAsync();

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

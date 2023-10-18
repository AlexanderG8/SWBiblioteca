using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SWBiblioteca.Services.Contract;
using System.Security.Claims;
using SWBiblioteca.Models;
using SWBiblioteca.Resources;

namespace SWBiblioteca.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(PERSONA model)
        {
            model.Clave = Utilities.EncryptKey(model.Clave);
            model.IdTipoPersona = 3;
            model.Estado = true;
            model.FechaCreacion = DateTime.Now;

            PERSONA newUsuario = await _usuarioService.SaveUsuario(model);
            if (newUsuario.IdPersona > 0)
            {
                return RedirectToAction("SignIn", "Login");
            }
            ViewData["Mensaje"] = "No se pudo registrar el usuario";
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string correo, string clave)
        {
            PERSONA usuario = await _usuarioService.GetUsuario(correo, Utilities.EncryptKey(clave));
            var nombreUsuario = string.Empty;
            if (usuario == null)
            {
                ViewData["Mensaje"] = "Correo o contraseña incorrectas";
                return View();
            }
            else 
            {
                nombreUsuario = usuario.Nombre + " " + usuario.Apellido;
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, nombreUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    properties
                );

            if (usuario.IdTipoPersona == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (usuario.IdTipoPersona == 2)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (usuario.IdTipoPersona == 2)
            {
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ViewData["Mensaje"] = "Error al iniciar sesión";
                return View();
            }
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("SignIn", "Login");
        }
    }
}

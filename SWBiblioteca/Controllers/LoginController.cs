using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SWBiblioteca.Services.Contract;
using System.Security.Claims;
using SWBiblioteca.Models;
using SWBiblioteca.Resources;
using SWBiblioteca.Clases;
using System.Numerics;

namespace SWBiblioteca.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEmailService _emailService;

        public LoginController(IUsuarioService usuarioService, IEmailService emailService)
        {
            _usuarioService = usuarioService;
            _emailService = emailService;
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
            model.Estado = false;
            model.FechaCreacion = DateTime.Now;

            #region Validar
            var validacion = await _usuarioService.ValidarUserExistente(model.Correo, model.Codigo);
            if (validacion == "Correo")
            {
                ViewData["Mensaje"] = "El correo ya se encuentra registrado";
                return View();
            }
            if (validacion == "DNI")
            {
                ViewData["Mensaje"] = "El DNI o Carné ya se encuentra registrado";
                return View();
            }
            #endregion

            PERSONA newUsuario = await _usuarioService.SaveUsuario(model);
            #region Envio de Correo
            if (newUsuario.IdPersona > 0)
            {
                var validador = _usuarioService.ConsultaValidador("Validador");
                foreach (var item in validador.Result)
                {
                    var cuerpo = $"<p>Estimado {item.Nombre} {item.Apellido} favor de validar los datos del siguiente usuario</p>"
                        + "<ul>"
                            + $"<li>Nombre Completo: {newUsuario.Nombre} {newUsuario.Apellido}</li>"
                            + $"<li>DNI o Carné de Extrangeria: {newUsuario.Codigo}"
                            + $"<li>Correo: {newUsuario.Correo}"
                        + "</ul>";

                    var request = new EmailDTO();
                    request.For = item.Correo;
                    request.Affair = "XANDER BIBLIOTECA: VALIDAR NUEVO USUARIO";
                    request.Content = Util.EmailBody(cuerpo);
                    _emailService.SendEmail(request);
                }
                return RedirectToAction("SignIn", "Login");
            }
            #endregion

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
            var codigo = "";
            if (usuario != null)
            {
                codigo = usuario.Codigo;
            }
            LOG_PERSONA consulta = await _usuarioService.EstadoUsuario(codigo);
            var nombreUsuario = string.Empty;
            if (usuario == null)
            {
                ViewData["Mensaje"] = "Correo o contraseña incorrectas!";
                return View();
            } else if (consulta != null ) 
            {
                ViewData["Mensaje"] = "Usuario en proceso de validación!";
                return View();
            }
            else
            {
                nombreUsuario = usuario.Nombre + " " + usuario.Apellido;
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario.Codigo)
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
            else if (usuario.IdTipoPersona == 4)
            {
                return RedirectToAction("Index", "Persona");
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

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string correo) 
        {
            var model = await _usuarioService.ValidarCorreo(correo);
            if (model == null)
            {
                ViewData["Mensaje"] = "Correo no se encuentra registrado!";
                return View();
            }

            var newClave = Util.GenerateRandomKey(8);
            model.Clave = Utilities.EncryptKey(newClave);
            await _usuarioService.EditUsuario(model);

            var cuerpo = $"<p>Estimado {model.Nombre} {model.Apellido}, su nueva contraseña es:</p>"
                        + "<ul>"
                            + $"<li>Contraseña: {newClave}</li>"
                        + "</ul>";

            var request = new EmailDTO();
            request.For = model.Correo;
            request.Affair = "XANDER BIBLIOTECA: VALIDAR NUEVO USUARIO";
            request.Content = Util.EmailBody(cuerpo);
            _emailService.SendEmail(request);

            return RedirectToAction("SignIn", "Login");
        }
    }
}

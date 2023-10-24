using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWBiblioteca.Data;
using SWBiblioteca.Entity;
using SWBiblioteca.Models;
using SWBiblioteca.Resources;

namespace SWBiblioteca.Controllers
{
    public class PersonaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonaController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<ActionResult> Index(DateTime? fecInicio, DateTime? fecFinal) 
        {
            #region Filtros
            //var fechaActual = DateTime.Now.Date;
            //var inicioMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
            //var finalMes = inicioMes.AddMonths(1).AddDays(-1);

            fecInicio = fecInicio == null ? null : fecInicio;
            fecFinal = fecFinal == null ? null : fecFinal;
            #endregion

            var lista = await _context.PR_PERSONA_Q01.FromSqlRaw("PR_PERSONA_Q01 @p0,@p1", fecInicio, fecFinal).ToListAsync();

            return View(lista);
        }

        public async Task<ActionResult> Modal(int id)
        {
            var model = new PERSONA();
            if (id > 0)
            {
                model = await _context.PERSONA.FindAsync(id);
            }

            #region Combos
            var estados = _context.ESTADOS.ToList();
            ViewData["Lista_Estados"] = new SelectList(estados.OrderBy(t => t.Descripcion), "Valor", "Descripcion", model.Estado);

            var tipoPersonas = _context.TIPO_PERSONA.Where(z => z.Estado).ToList();
            ViewData["Lista_TipPersonas"] = new SelectList(tipoPersonas.OrderBy(t => t.Descripcion), "IdTipoPersona", "Descripcion", model.IdTipoPersona);
            #endregion

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(PERSONA persona)
        {
            try
            {
                var model = await _context.PERSONA.FindAsync(persona.IdPersona);
                if (model != null)
                {
                    model.Nombre = persona.Nombre;
                    model.Apellido = persona.Apellido;
                    model.Correo = persona.Correo;
                    if (!String.IsNullOrEmpty(persona.NewClave))
                    {
                        model.Clave = Utilities.EncryptKey(persona.Clave);
                    }
                    model.Codigo = persona.Codigo;
                    model.IdTipoPersona = persona.IdTipoPersona;
                    model.Estado = persona.Estado;
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (!String.IsNullOrEmpty(persona.NewClave))
                    {
                        persona.Clave = Utilities.EncryptKey(persona.NewClave);
                    }
                    persona.FechaCreacion = DateTime.Now.Date;
                    _context.Add(persona);
                    await _context.SaveChangesAsync();
                }
                return Json(new { valor = true, message = "Tu categoría se guardo con exito!" });
            }
            catch (Exception ex)
            {
                return Json(new { valor = false, message = ex.Message });
            }
        }

        public async Task<ActionResult> ModalResetPassword(int id)
        {
            var model = new PERSONA();
            if (id > 0)
            {
                model = await _context.PERSONA.FindAsync(id);
            }
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(int id, string password)
        {
            try
            {
                var model = await _context.PERSONA.FindAsync(id);
                if (model != null)
                {
                    if (!String.IsNullOrEmpty(password))
                    {
                        model.Clave = Utilities.EncryptKey(password);
                    }
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    return Json(new { valor = true, message = "Se cambio la contraseña con éxito!" });
                }
                else 
                {
                    return Json(new { valor = false, message = "No se pudo cambiar la contraseña!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { valor = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                if (id > 0)
                {
                    var model = await _context.PERSONA.FindAsync(id);

                    _context.PERSONA.Remove(model);
                    await _context.SaveChangesAsync();
                }
                return Json(new { valor = true, message = "EL usuario seleccionado se eliminó con exito!" });
            }
            catch (Exception ex)
            {
                return Json(new { valor = false, message = ex.Message });
            }
        }

    }
}

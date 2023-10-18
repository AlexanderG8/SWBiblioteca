using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWBiblioteca.Data;
using SWBiblioteca.Entity;

namespace SWBiblioteca.Controllers
{
    [Authorize]
    public class EditorialController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EditorialController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var lista = await _context.EDITORIAL.ToListAsync();
            return View(lista);
        }

        public async Task<ActionResult> Modal(int id)
        {
            var model = new EDITORIAL();
            if (id > 0)
            {
                model = await _context.EDITORIAL.FindAsync(id);
            }
            var estados = _context.ESTADOS.ToList();
            ViewData["Lista_Estados"] = new SelectList(estados.OrderBy(t => t.Descripcion), "Valor", "Descripcion", model.Estado);

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(EDITORIAL editorial)
        {
            try
            {
                var model = await _context.EDITORIAL.FindAsync(editorial.IdEditorial);
                if (model != null)
                {
                    model.Descripcion = editorial.Descripcion;
                    model.Estado = editorial.Estado;
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    editorial.FechaCreacion = DateTime.Now.Date;
                    _context.Add(editorial);
                    await _context.SaveChangesAsync();
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                if (id > 0)
                {
                    var model = await _context.EDITORIAL.FindAsync(id);

                    _context.EDITORIAL.Remove(model);
                    await _context.SaveChangesAsync();
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
    }
}

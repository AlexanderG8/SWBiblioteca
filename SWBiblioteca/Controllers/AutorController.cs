using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWBiblioteca.Data;
using SWBiblioteca.Entity;

namespace SWBiblioteca.Controllers
{
    [Authorize]
    public class AutorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var lista = await _context.AUTOR.ToListAsync();
            return View(lista);
        }

        public async Task<ActionResult> Modal(int id)
        {
            var model = new AUTOR();
            if (id > 0)
            {
                model = await _context.AUTOR.FindAsync(id);
            }
            var estados = _context.ESTADOS.ToList();
            ViewData["Lista_Estados"] = new SelectList(estados.OrderBy(t => t.Descripcion), "Valor", "Descripcion", model.Estado);

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(AUTOR autor)
        {
            try
            {
                var model = await _context.AUTOR.FindAsync(autor.IdAutor);
                if (model != null)
                {
                    model.Descripcion = autor.Descripcion;
                    model.Estado = autor.Estado;
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    autor.FechaCreacion = DateTime.Now.Date;
                    _context.Add(autor);
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
                    var model = await _context.AUTOR.FindAsync(id);

                    _context.AUTOR.Remove(model);
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

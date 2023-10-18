using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWBiblioteca.Data;
using SWBiblioteca.Entity;

namespace SWBiblioteca.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var lista = await _context.CATEGORIA.ToListAsync();
            return View(lista);
        }

        public async Task<ActionResult> Modal(int id) 
        {
            var model = new CATEGORIA();
            if (id > 0)
            {
                model = await _context.CATEGORIA.FindAsync(id);
            }
            var estados = _context.ESTADOS.ToList();
            ViewData["Lista_Estados"] = new SelectList(estados.OrderBy(t => t.Descripcion), "Valor", "Descripcion", model.Estado);

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(CATEGORIA categoria) 
        {
            try
            {
                var model = await _context.CATEGORIA.FindAsync(categoria.IdCategoria);
                if (model != null)
                {
                    model.Descripcion = categoria.Descripcion;
                    model.Estado = categoria.Estado;
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    categoria.FechaCreacion = DateTime.Now.Date;
                    _context.Add(categoria);
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
                    var model = await _context.CATEGORIA.FindAsync(id);

                    _context.CATEGORIA.Remove(model);
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

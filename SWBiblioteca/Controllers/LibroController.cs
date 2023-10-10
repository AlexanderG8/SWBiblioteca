using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWBiblioteca.Data;
using SWBiblioteca.Entity;

using SWBiblioteca.Clases;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;

namespace SWBiblioteca.Controllers
{
    public class LibroController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public LibroController(ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<ActionResult> Index()
        {
            var lista = await _context.LISTAR_LIBROS_Q01.FromSqlRaw("LISTAR_LIBROS_Q01").ToListAsync();
            return View(lista);
        }

        public async Task<ActionResult> Modal(int id)
        {
            var model = new LIBRO();
            if (id > 0)
            {
                model = await _context.LIBRO.FindAsync(id);
            }
            #region Combos
            var estados = _context.ESTADOS.ToList();
            estados.Add(new ESTADOS { Id_Estado = 0, Descripcion = "Seleccionar" });
            ViewData["Lista_Estados"] = new SelectList(estados.OrderBy(t => t.Descripcion), "Valor", "Descripcion", model.Estado);

            var autores = _context.AUTOR.Where(z => z.Estado).ToList();
            autores.Add(new AUTOR { IdAutor = 0, Descripcion = "Seleccionar" });
            ViewData["Lista_Autores"] = new SelectList(autores.OrderBy(t => t.Descripcion), "IdAutor", "Descripcion", model.IdAutor);

            var categorias = _context.CATEGORIA.Where(z => z.Estado).ToList();
            categorias.Add(new CATEGORIA { IdCategoria = 0, Descripcion = "Seleccionar" });
            ViewData["Lista_Categorias"] = new SelectList(categorias.OrderBy(t => t.Descripcion), "IdCategoria", "Descripcion", model.IdCategoria);

            var editoriales = _context.EDITORIAL.Where(z => z.Estado).ToList();
            editoriales.Add(new EDITORIAL { IdEditorial = 0, Descripcion = "Seleccionar" });
            ViewData["Lista_Editoriales"] = new SelectList(editoriales.OrderBy(t => t.Descripcion), "IdEditorial", "Descripcion", model.IdEditorial);
            #endregion

            return PartialView(model);
        }


        [HttpPost]
        public async Task<IActionResult> Guardar(string objeto, IFormFile imagen)
        {
            try
            {
                var libro = JsonConvert.DeserializeObject<LIBRO>(objeto);
                var model = await _context.LIBRO.FindAsync(libro.IdLibro);
                if (model != null)
                {
                    model.Titulo = libro.Titulo;
                    model.IdAutor = libro.IdAutor;
                    model.IdCategoria = libro.IdCategoria;
                    model.IdEditorial = libro.IdEditorial;
                    model.Ubicacion = libro.Ubicacion;
                    model.Ejemplares = libro.Ejemplares;
                    model.Estado = libro.Estado;
                    if (imagen != null)
                    {
                        List<string> lista = await Util.UploadDocument(_hostingEnvironment, imagen, "ImgLibros/");
                        model.NombrePortada = lista[0];
                        model.RutaPortada = lista[1];
                    }
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (imagen != null)
                    {
                        List<string> lista = await Util.UploadDocument(_hostingEnvironment, imagen, "ImgLibros/");
                        libro.NombrePortada = lista[0];
                        libro.RutaPortada = lista[1];
                    }
                    libro.FechaCreacion = DateTime.Now.Date;
                    _context.Add(libro);
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

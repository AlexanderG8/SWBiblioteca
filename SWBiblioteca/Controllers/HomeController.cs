﻿using Microsoft.AspNetCore.Mvc;
using SWBiblioteca.Data;
using SWBiblioteca.Models;
using System.Diagnostics;

namespace SWBiblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            #region Cantidades
            var libros = _context.LIBRO.Where(z => z.Estado).ToList().Count();
            #endregion

            #region ViewBags
            ViewBag.CantidadLibros = libros;
            #endregion

            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SWBiblioteca.Models;
using System.Diagnostics;

namespace SWBiblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
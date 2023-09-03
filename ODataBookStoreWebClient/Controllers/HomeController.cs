using Microsoft.AspNetCore.Mvc;
using ODataBookStoreWebClient.Models;
using System.Diagnostics;

namespace ODataBookStoreWebClient.Controllers
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
            return RedirectToAction("Index", "Book");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
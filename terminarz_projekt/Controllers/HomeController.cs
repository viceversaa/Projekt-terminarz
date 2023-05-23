using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using terminarz_projekt.Models;

namespace terminarz_projekt.Controllers
{
    /// <summary>
    /// Kontroler obslugujacy widoki strony glownej/informacyjnej.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Funkcja zwracajaca widok.
        /// </summary>
        /// <returns>Widok Index.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Funkcja zwracajaca widok.
        /// </summary>
        /// <returns>Widok Privacy.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        /// <summary>
        /// Funkcja zwracajaca widok.
        /// </summary>
        /// <returns>Widok Error.</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
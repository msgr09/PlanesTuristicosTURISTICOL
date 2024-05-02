using PlanesTuristicos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PlanesTuristicos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace PlanesTuristicos.Controllers
{

  
    public class Home3Controller : Controller
    {
        private readonly ILogger<Home3Controller> _logger;

        public Home3Controller(ILogger<Home3Controller> logger)
        {
            _logger = logger;
        }
       
        public IActionResult Index3()
        {
            return View();
        }

        public IActionResult Privacy3()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
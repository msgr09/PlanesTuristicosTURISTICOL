using PlanesTuristicos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PlanesTuristicos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PlanesTuristicos.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       public IActionResult Index()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            string nombreusuario = "";

            if (claimuser.Identity.IsAuthenticated)
            {
                nombreusuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
            }

            ViewData["nombreUsuario"] = nombreusuario;

            return View();
        }



        public IActionResult Privacy()
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
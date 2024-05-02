using PlanesTuristicos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PlanesTuristicos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace PlanesTuristicos.Controllers
{

  
    public class Home2Controller : Controller
    {
        private readonly ILogger<Home2Controller> _logger;

        public Home2Controller(ILogger<Home2Controller> logger)
        {
            _logger = logger;
        }
       
        public IActionResult Index2()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            string nombreProveedo = "";

            if (claimuser.Identity.IsAuthenticated)
            {
                nombreProveedo = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
            }

            ViewData["nombreProveedo"] = nombreProveedo;

            return View();
        }

        public IActionResult Privacy2()
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
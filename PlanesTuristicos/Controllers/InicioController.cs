using Microsoft.AspNetCore.Mvc;
using PlanesTuristicos.Models;
using PlanesTuristicos.Recursos;
using PlanesTuristicos.Servicios.Contrato;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using PlanesTuristicos.Data;
using System.Linq;
using System.Threading.Tasks;
using PlanesTuristicos.Servicios.Implementacion;
using System.Numerics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace PlanesTuristicos.Controllers


{

    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;
        private readonly IProveedorService _proveedorService;
        private readonly IPlanesTService _planesTServicio;
        private readonly IReservaService _reservaService;

        private readonly PlanesTuristicosContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InicioController(IUsuarioService usuarioServicio, IProveedorService proveedorService, IPlanesTService planesTServicio, PlanesTuristicosContext context, IHttpContextAccessor httpContextAccessor, IReservaService reservaService)
        {
            _usuarioServicio = usuarioServicio;
            _proveedorService = proveedorService;
            _planesTServicio = planesTServicio;
            _reservaService = reservaService;

            _context = context;
            _httpContextAccessor = httpContextAccessor;


        }
        public IActionResult Elegir()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Elegir(string tipo)
        {
            if (tipo == "Turista")
            {
                return RedirectToAction("IniciarSesionTurista", "Inicio");

            }
            else if (tipo == "Proveedor")
            {
                return RedirectToAction("IniciarSesionProveedor", "Inicio");

            }
            else if (tipo == "Administrador")
            {
                return RedirectToAction("IniciarSesionAdmin", "Inicio");
            }
            else
            {
                return RedirectToAction("ElegirTipo");
            }
        }

        public IActionResult Registrarse()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            modelo.Clave = Utilidades.EncriptarClave(modelo.Clave);
            Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);

            if (usuario_creado.IdUsuario > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        public IActionResult Registrarse2()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Registrarse2(Proveedor modelo1)
        {
            modelo1.Clave = Utilidades.EncriptarClave(modelo1.Clave);
            Proveedor proveedor_creado = await _proveedorService.SaveProveedor(modelo1);

            if (proveedor_creado.Id_Proveedor > 0)

                return RedirectToAction("IniciarSesion2", "Inicio");



            ViewData["Mensaje"] = "No se pudo crear el usuario";

            return View();

        }
        public IActionResult GuardarPlan()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> GuardarPlan(PlanesT modelo2, int idProveedor)
        {
            // Obtener el proveedor por correo electrónico
            var correoElectronico = User.FindFirstValue("CorreoElectronico");

            // Verificar si el correo electrónico está presente
            if (string.IsNullOrEmpty(correoElectronico))
            {
                // Manejar el caso en que el correo electrónico no esté presente en los claims
                return RedirectToAction("Error", "Home");
            }

            // Obtener el proveedor por correo electrónico
            Proveedor proveedor = await _proveedorService.GetProveedorPorCorreo(correoElectronico);

            if (proveedor == null)
            {
                // Manejar el caso donde no se encuentra el proveedor
                ViewData["Mensaje"] = "No se encontró el proveedor asociado al correo electrónico actual.";
                return View();
            }

            // Asignar el proveedor al plan turístico
            modelo2.Proveedor = proveedor;

            // Guardar el plan turístico en la base de datos
            PlanesT planesT_creado = await _planesTServicio.SavePlanesT(modelo2);

            if (planesT_creado.Id_PlanTuristicos > 0)
            {
                ViewData["Mensaje"] = "SE GUARDÓ EL PLAN TURÍSTICO";
            }

            return View();
        }
        public IActionResult IniciarSesion()
        {
            return View();

        }

        public async Task<IActionResult> IniciarSesionTurista(string Correo, string Clave)
        {
            

            // Cerrar sesión antes de iniciar una nueva sesión
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Obtener el usuario encontrado
            var usuario_encontrado = await _usuarioServicio.GetUsuario(Correo, Utilidades.EncriptarClave(Clave));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            // Crear los claims para el usuario
            var claims = new List<Claim>
    {

        new Claim("CorreoElectronico", usuario_encontrado.Correo)
        // Agregar otros claims según sea necesario
    };

            // Crear el objeto ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties
            {
                AllowRefresh = true
            };

            // Iniciar sesión con el objeto ClaimsPrincipal
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            // Redirigir a la página de inicio
            return RedirectToAction("Index", "Home");
        }


        public IActionResult IniciarSesion2()
        {
            return View();

        }

        [HttpPost]

        public async Task<IActionResult> IniciarSesionProveedor(String Correo, String Clave, int Rut)

        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Obtener el usuario encontrado
            var proveedorencontrado = await _proveedorService.GetProveedor(Correo, Utilidades.EncriptarClave(Clave), Rut);

            if (proveedorencontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            // Crear los claims para el usuario
            var claims = new List<Claim>
    {

        new Claim("CorreoElectronico", proveedorencontrado.Correo)
        // Agregar otros claims según sea necesario
    };

            // Crear el objeto ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties
            {
                AllowRefresh = true
            };

            // Iniciar sesión con el objeto ClaimsPrincipal
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            // Redirigir a la página de inicio
            return RedirectToAction("Index2", "Home2");
        }
        public IActionResult IniciarSesion3()
        {
            return View();

        }
        [HttpPost]

        public async Task<IActionResult> IniciarSesionAdmin(String Correo, String Clave, Admin _admin)

        {
            DA_Logica _da_admin = new DA_Logica();
            Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(Correo, Utilidades.EncriptarClave(Clave));

            var admin = _da_admin.ValidarAdmin(_admin.Correo, _admin.Clave);
            if (admin != null)
            {
                return RedirectToAction("Index3", "Home3");

            }

            if (usuario_encontrado == null)
            {

                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }
            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuario_encontrado.NombreTurista)


               };


            {

            }
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true

            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");



        }
        [HttpPost]
        [Authorize] // Asegura que solo los usuarios autenticados puedan reservar
        public async Task<IActionResult> Reservar(int idPlanTuristico, string NombrePlanTuristico, string informacion, string actividades, string municipio, byte[] imagen, double precio, string nombreUsuario, string correoUsuario, string telefonoUsuario)
        {
            // Obtener el correo electrónico actual del usuario desde los claims
            var correoElectronico = User.FindFirstValue("CorreoElectronico");

            // Verificar si el correo electrónico está presente
            if (string.IsNullOrEmpty(correoElectronico))
            {
                // Manejar el caso en que el correo electrónico no esté presente en los claims
                return RedirectToAction("Error", "Home");
            }

            // Realizar la reserva utilizando el correo electrónico del usuario actual
            var exito = await _reservaService.RealizarReserva(idPlanTuristico, NombrePlanTuristico,informacion,actividades,municipio,imagen,precio ,nombreUsuario, correoElectronico, telefonoUsuario);

            if (exito)
            {
                // Redirigir a alguna página de confirmación o a donde prefieras
                return RedirectToAction("Reservas");
            }
            else
            {
                // Manejar el caso de fallo en la reserva
                // Puedes mostrar un mensaje de error o redirigir a una página de error
                return RedirectToAction("Error", "Home");
            }
        }







        public IActionResult Reservas()
        {

            return View();
        }
        public async Task<IActionResult> MostrarPlanes()
        {
            // Verificar si el usuario tiene una sesión activa
            if (!User.Identity.IsAuthenticated)
            {
                // Redirigir al usuario a iniciar sesión si no tiene una sesión activa
                return RedirectToAction("Index", "Home");
            }

            // Obtener el correo electrónico actual del usuario desde los claims
            var correoElectronico = User.FindFirstValue("CorreoElectronico");

            // Utilizar el servicio de usuario para obtener los detalles del usuario basados en el correo electrónico
            var usuario = await _usuarioServicio.GetUsuarioPorCorreo(correoElectronico);

            // Verificar si se encontró el usuario
            if (usuario == null)
            {
                // Manejar el caso en que el usuario no se encuentra en la base de datos
                return RedirectToAction("Perfil1");
            }

            // Ahora puedes continuar con la lógica para mostrar los planes
            var viewModel = new IndexViewModel
            {
                Usuarios = await _usuarioServicio.ObtenerTurista(),
                Planes = await _planesTServicio.ObtenerPlanesTuristicos()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePlanAndReserva(int idPlan, int idReserva)
        {
            // Eliminar el plan
            var plan = _context.PlanesT.Find(idPlan);
            if (plan != null)
            {
                _context.PlanesT.Remove(plan);
            }

            // Eliminar la reserva
            var reserva = _context.Reserva.Find(idReserva);
            if (reserva != null)
            {
                _context.Reserva.Remove(reserva);
            }

            _context.SaveChanges();

            return RedirectToAction("PerfilProveedor","Inicio");
        }

        public async Task<IActionResult> MostrarTuristas()
        {


            var usuarios = await _usuarioServicio.ObtenerTurista();


            return View(usuarios);

        }
        public async Task<IActionResult> MostrarProveedor()
        {


            var usuarios = await _proveedorService.ObtenerProveedor();


            return View(usuarios);

        }

        public async Task<IActionResult> MirarPlanes()
        {


            var planesT = await _planesTServicio.ObtenerPlanes();


            return View(planesT);
        }

        [Authorize] // Solo usuarios autenticados pueden acceder a esta acción
        public async Task<IActionResult> Perfil()
        {
            // Obtener el correo electrónico actual del usuario desde los claims
            var correoElectronico = User.FindFirstValue("CorreoElectronico");

            if (string.IsNullOrEmpty(correoElectronico))
            {
                // Si no se encuentra el correo electrónico en los claims, mostrar un mensaje de error
                ViewData["Mensaje"] = "No se encontró el correo electrónico del usuario.";
                return View();
            }

            // Utilizar el servicio de usuario para obtener los detalles del usuario basados en el correo electrónico
            var usuario = await _usuarioServicio.GetUsuarioPorCorreo(correoElectronico);

            if (usuario == null)
            {
                // Si no se encuentra el usuario en la base de datos, mostrar un mensaje de error
                ViewData["Mensaje"] = "No se encontró el usuario en la base de datos.";
                return View();
            }

            // Obtener las reservas del usuario
            var reservasUsuario = await _reservaService.ObtenerReservasPorCorreo(correoElectronico);

            // Crear el objeto PerfilViewModel y pasar los datos del usuario y sus reservas
            var viewModel = new Index2ViewModel
            {
                Usuario = usuario,
                Reservas = reservasUsuario
            };

            // Pasar el modelo PerfilViewModel a la vista
            return View(viewModel);
        }

        
        [Authorize]
        public async Task<IActionResult> PerfilProveedor(){
            // Obtener el correo electrónico del proveedor desde la cookie
            // Obtener el correo electrónico actual del proveedor desde los claims
            var correoElectronico = User.FindFirstValue("CorreoElectronico");

            // Verificar si el correo electrónico está presente
            if (string.IsNullOrEmpty(correoElectronico))
            {
                // Manejar el caso en que el correo electrónico no esté presente en los claims
                return RedirectToAction("Error", "Home");
            }

            // Obtener información del proveedor desde la base de datos
            var proveedor = await _proveedorService.GetProveedorPorCorreo(correoElectronico);

            if (proveedor == null)
            {
                // Manejar el caso donde no se encuentra el proveedor
                return RedirectToAction("Error", "Home");
            }

            // Obtener los planes turísticos asociados al proveedor desde la base de datos
            var planesTuristicos = await _planesTServicio.ObtenerPlanesPorProveedor(proveedor.Id_Proveedor);

            // Crear el objeto de modelo para la vista
            var viewModel = new PerfilProveedorViewModel
            {
                Proveedor = proveedor,
                PlanesTuristicos = planesTuristicos
            };

            // Pasar el modelo PerfilProveedorViewModel a la vista
            return View("PerfilProveedor", viewModel);
        }


       



        public async Task<IActionResult> Salir()
        {
            // Limpiar la sesión actual del usuario
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirigir al usuario a la página de inicio de sesión
            return RedirectToAction("Elegir", "Inicio");
        }
    }
}


     
    








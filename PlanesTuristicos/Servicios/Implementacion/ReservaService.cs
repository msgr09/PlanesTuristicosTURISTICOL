using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlanesTuristicos.Models;
using PlanesTuristicos.Servicios.Contrato;

namespace PlanesTuristicos.Services
{
    public class ReservaService : IReservaService
    {
        private readonly PlanesTuristicosContext _context;

        public ReservaService(PlanesTuristicosContext context)
        {
            _context = context;
        }

        public async Task<bool> RealizarReserva(int idPlanTuristico, string NombrePlanTuristico, string informacion, string actividades, string municipio, byte[] imagen, double precio, string nombreUsuario, string correoUsuario, string telefonoUsuario)
        {
            try
            {
                // Verifica si el plan turístico existe
                var planTuristico = await _context.PlanesT.FindAsync(idPlanTuristico);
                if (planTuristico == null)
                {
                    return false;
                }

                // Crea una nueva reserva
                var reserva = new Reserva
                {
                    IdPlanTuristico = idPlanTuristico,
                    NombrePlanTuristico = NombrePlanTuristico,
                    Informacion = informacion,
                    Actividades = actividades,
                    Municipio = municipio,
                    Imagen= imagen,
                    Precio = precio,
                    NombreUsuario = nombreUsuario,
                    CorreoUsuario = correoUsuario,
                    TelefonoUsuario = telefonoUsuario,
                    FechaReserva = DateTime.Now // Puedes ajustar la fecha de la reserva según tus necesidades
                };

                // Guarda la reserva en la base de datos
                _context.Reserva.Add(reserva);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                // Maneja cualquier error que pueda ocurrir durante la reserva
                return false;
            }
        }
        public async Task<IEnumerable<Reserva>> ObtenerReservasPorCorreo(string correoUsuario)
        {
            return await _context.Reserva.Where(r => r.CorreoUsuario == correoUsuario).ToListAsync();
        }

    }
}
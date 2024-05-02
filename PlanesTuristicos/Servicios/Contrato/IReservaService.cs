using PlanesTuristicos.Models;

namespace PlanesTuristicos.Servicios.Contrato
{
    public interface IReservaService
    {
        Task<bool> RealizarReserva(int idPlanTuristico,string NombrePlanTuristico ,string informacion, string actividades,string municipio, byte[] imagen , double precio,string nombreUsuario, string correoUsuario, string telefonoUsuario);
        Task<IEnumerable<Reserva>> ObtenerReservasPorCorreo(string correoUsuario);
    }

}

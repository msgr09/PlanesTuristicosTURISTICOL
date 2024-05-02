using Microsoft.EntityFrameworkCore;
using PlanesTuristicos.Models;

namespace PlanesTuristicos.Servicios.Contrato
{
    public interface IPlanesTService
    {
        Task<PlanesT> GetPlanesT(string nombre_PlanT, string ubicacion, int rut,double precio,string duracion,int actividades, string informacion, byte[] imagen);
        Task<PlanesT> SavePlanesT(PlanesT modelo2);
        Task<List<PlanesT>> ObtenerPlanesTuristicos();

        Task<List<PlanesT>> ObtenerPlanes();
        Task<List<PlanesT>> ObtenerPlanesPorProveedor(int idProveedor);
    }
}

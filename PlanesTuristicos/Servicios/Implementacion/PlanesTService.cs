using Microsoft.EntityFrameworkCore;
using PlanesTuristicos.Models;
using PlanesTuristicos.Servicios.Contrato;

namespace PlanesTuristicos.Servicios.Implementacion
{
    public class PlanesTService : IPlanesTService
    {
        private readonly PlanesTuristicosContext _dbcontext;

        public PlanesTService(PlanesTuristicosContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<PlanesT> GetPlanesT(string nombre_PlanT, string ubicacion, int rut, double precio, string duracion, int actividades, string informacion, byte[] imagen)
        {
            PlanesT planesT_encontrado = await _dbcontext.PlanesT.Where(u => u.Nombre_PlanTuristico == nombre_PlanT && u.Municipio == ubicacion && u.Rut == rut && u.Precio == precio && u.Duracion==duracion && u.Actividades == actividades && u.Informacion == informacion && u.Imagen == imagen).FirstOrDefaultAsync();

            return planesT_encontrado;
        }

        public async Task<PlanesT> SavePlanesT(PlanesT modelo2)
        {
            _dbcontext.PlanesT.Add(modelo2);
            await _dbcontext.SaveChangesAsync();
            return modelo2;
        }
        public async Task<List<PlanesT>> ObtenerPlanesTuristicos()
        {
            return await _dbcontext.PlanesT.ToListAsync();
        }

        public async Task<List<PlanesT>> ObtenerPlanes()
        {
            return await _dbcontext.PlanesT.ToListAsync();
        }

        public async Task<List<PlanesT>> ObtenerPlanesPorProveedor(int idProveedor)
        {
            return await _dbcontext.PlanesT.Where(p => p.IdProveedor == idProveedor).ToListAsync();
        }
    }

        
}

using Microsoft.EntityFrameworkCore;
using PlanesTuristicos.Models;
using PlanesTuristicos.Servicios.Contrato;

namespace PlanesTuristicos.Servicios.Implementacion
{
    public class ProveedorService : IProveedorService
    {
        private readonly PlanesTuristicosContext _dbcontext;

        public ProveedorService(PlanesTuristicosContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Proveedor> GetProveedor(string correo, string clave, int rut  )
        {
            Proveedor proveedor_encontrado = await _dbcontext.Proveedor.Where(u => u.Correo == correo && u.Clave == clave && u.Rut == rut).FirstOrDefaultAsync();  

            return proveedor_encontrado;
        }

        public  async Task<Proveedor> SaveProveedor(Proveedor modelo1)
        {
            _dbcontext.Proveedor.Add(modelo1);
            await _dbcontext.SaveChangesAsync();
            return modelo1;
        }

        public async Task<List<Proveedor>> ObtenerProveedor()
        {
            return await _dbcontext.Proveedor.ToListAsync();
        }
        public async Task<Proveedor> GetProveedorPorCorreo(string correo)
        {
            return await _dbcontext.Proveedor.FirstOrDefaultAsync(u => u.Correo == correo);
        }
    }

}

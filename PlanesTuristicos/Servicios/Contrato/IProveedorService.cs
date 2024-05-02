using PlanesTuristicos.Models;

namespace PlanesTuristicos.Servicios.Contrato
{
    public interface IProveedorService
    {
        Task<Proveedor> GetProveedor(string correo, string clave, int rut);
        Task<Proveedor> SaveProveedor(Proveedor modelo1);
        Task<List<Proveedor>> ObtenerProveedor();

        Task<Proveedor> GetProveedorPorCorreo(string correo);
    }
}

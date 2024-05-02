using Microsoft.EntityFrameworkCore;
using PlanesTuristicos.Models;
using System.Threading.Tasks;


namespace PlanesTuristicos.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        
        Task<Usuario> SaveUsuario(Usuario modelo);
        Task<List<Usuario>> ObtenerTurista();
        Task<Usuario> GetUsuarioById(int idUsuario);
        Task<Usuario> GetUsuarioPorCorreo(string correo);


    }



}

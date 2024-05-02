using Microsoft.EntityFrameworkCore;
using PlanesTuristicos.Models;
using PlanesTuristicos.Servicios.Contrato;
using System.Collections.Generic;

namespace PlanesTuristicos.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        
        private readonly PlanesTuristicosContext _dbcontext;

        public UsuarioService(PlanesTuristicosContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario_encontrado = await _dbcontext.Usuarios.Where(u => u.Correo == correo && u.Clave == clave).FirstOrDefaultAsync();

            return usuario_encontrado;
        }
        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _dbcontext.Usuarios.FindAsync(id);
        }


        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbcontext.Usuarios.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return modelo;
        }
        public async Task<List<Usuario>> ObtenerTurista()
        {
            return await _dbcontext.Usuarios.ToListAsync();
        }
        public async Task<Usuario> GetUsuarioPorCorreo(string correo)
        {
            return await _dbcontext.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<IEnumerable<Usuario>> GetUsuarioPorId(int idUsuario)
        {
            return await _dbcontext.Usuarios.Where(r => r.IdUsuario == idUsuario).ToListAsync();
        }
    }
}
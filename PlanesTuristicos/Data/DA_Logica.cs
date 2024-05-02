using PlanesTuristicos.Models;

namespace PlanesTuristicos.Data
{
    public class DA_Logica
    {
        public List<Admin> ListaAdmin()
        {
            return new List<Admin>() {



                new Admin { NombreAdmin = "Marlon", Correo = "Marlon@gmail.com", Clave = "1234", Roles = new string[] { "Administrador" } },

             };


        }
        public Admin ValidarAdmin(string _correo, string _clave)
        {
            return ListaAdmin().Where(item => item.Correo == _correo && item.Clave == _clave).FirstOrDefault();
        }
    }
}


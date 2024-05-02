using System.ComponentModel.DataAnnotations.Schema;

namespace PlanesTuristicos.Models
{
    public class Admin
    {
        public int IdUsuario { get; set; }

        public string? NombreAdmin { get; set; }

        public string? Correo { get; set; }

        public string? Clave {  get; set; }

        [NotMapped]
        public string[] Roles { get; set; }


    }
}

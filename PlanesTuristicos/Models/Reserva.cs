using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanesTuristicos.Models
{
    public class Reserva
    {
        [Key] // Definición de la clave primaria
        public int IdReserva { get; set; }

        public int IdPlanTuristico { get; set; }
        public string NombrePlanTuristico { get; set; }

        public string Informacion{ get; set; }

        public string Actividades { get; set; }

        public string Municipio { get; set; }

        public byte[] Imagen { get; set; }

        public double Precio { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string TelefonoUsuario { get; set; }
        public DateTime FechaReserva { get; set; }

        // Relación con PlanesT
        [ForeignKey("IdPlanTuristico")]
        public PlanesT PlanTuristico { get; set; }
    }
}

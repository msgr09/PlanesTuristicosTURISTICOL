using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace PlanesTuristicos.Models
{
    public class PlanesT
    {
        [Key]
        public int Id_PlanTuristicos { get; set; }

        
        public string Nombre_PlanTuristico { get; set; }

        public int? Rut { get; set; }

        public string Municipio { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public double  Precio { get; set; }

        public int? Actividades { get; set; }

        public string Duracion { get; set; }

        public string Informacion { get; set; }

        public byte[] Imagen { get; set; }

        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public Proveedor Proveedor { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
    }
}

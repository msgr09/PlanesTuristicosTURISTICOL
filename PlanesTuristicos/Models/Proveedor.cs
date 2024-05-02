using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace PlanesTuristicos.Models

{
    public class Proveedor
    {
        public int Id_Proveedor { get; set; }

        public string nombreProveedor { get; set; }

        public int? Rut { get; set; }

        public int? Cedula { get; set; }

        public string? Correo { get; set; }

        public string? direccion { get; set; }
        public string? Clave { get; set; }



        public ICollection<PlanesT> Planes { get; set; }


    }
}

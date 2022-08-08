using System.ComponentModel.DataAnnotations;

namespace Core.Entidades
{
    public class Hospital
    {
        [Key]
        public int Id { get; set; }

        public string  NombreHospital { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Correo { get; set; }

    }
}
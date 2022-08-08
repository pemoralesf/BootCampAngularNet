using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entidades
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        public string  Apellidos { get; set; }

        public string  Nombres { get; set; }
        
        public string Direccion { get; set; }

        public string Telefono { get; set;}
        
        public string  Medico  { get; set; }

        public string  MotivoConsulta { get; set; }

        public int HospitalId { get; set; }

        [ForeignKey("HospitalId")]
        public Hospital Hospital { get; set; }
    }
}
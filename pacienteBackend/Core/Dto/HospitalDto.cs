using System.ComponentModel.DataAnnotations;

namespace Core.Dto
{
    public class HospitalDto
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage ="El Hospital es  requerido")]
        [MaxLength(100, ErrorMessage ="No debe ser  mayor  a  100")]
        public string  NombreHospital { get; set; }

        [Required(ErrorMessage ="La   Direccion del Hospital es  requerido")]
        [MaxLength(100, ErrorMessage ="No debe ser  mayor  a  150")]
        public string Direccion { get; set; }

        [Required(ErrorMessage ="El No. de  Telefono  del  Hospital es  requerido")]
        [MaxLength(100, ErrorMessage ="No debe ser  mayor  a  40")]
        public string Telefono { get; set; }

        [Required(ErrorMessage ="El correo  del  Hospital es  requerido")]
        [MaxLength(100, ErrorMessage ="No debe ser  mayor  a  40")]
        public string Correo { get; set; }
    }
}
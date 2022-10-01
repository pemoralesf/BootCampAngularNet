namespace Core.Dto
{
    public class PacienteUpsertDto
    {
         public int Id { get; set; }

        public string  Apellidos { get; set; }

        public string  Nombres { get; set; }
        
        public string Direccion { get; set; }

        public string Telefono { get; set;}
        
        public string  Medico  { get; set; }

        public string  MotivoConsulta { get; set; }

        public int HospitalId { get; set; }

    
    }
}
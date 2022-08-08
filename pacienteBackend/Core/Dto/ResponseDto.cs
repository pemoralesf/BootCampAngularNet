namespace Core.Dto
{
    public class ResponseDto
    {
        public bool IsExitoso { get; set; } = true;
        public object Resultado { get; set; }
        
        public String Mensaje { get; set; }
    }
}
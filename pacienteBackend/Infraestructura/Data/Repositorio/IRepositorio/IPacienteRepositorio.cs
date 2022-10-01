using Core.Entidades;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IPacienteRepositorio :IRepositorio<Paciente>
    {
         void Actualizar (Paciente paciente);
         
    }
}
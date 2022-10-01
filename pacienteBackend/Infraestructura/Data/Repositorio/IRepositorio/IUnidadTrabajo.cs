using Infraestructura.Data.Repositorio;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.IRepositorio 
{
    public interface IUnidadTrabajo :IDisposable
    {
         IHospitalRepositorio Hospital {get; }

         IPacienteRepositorio Paciente {get; }

         Task Guardar();
    }
}
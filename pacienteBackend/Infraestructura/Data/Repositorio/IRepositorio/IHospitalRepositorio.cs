using Core.Entidades;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IHospitalRepositorio :IRepositorio<Hospital>
    {
         void  Actualizar(Hospital hospital);
    }
}
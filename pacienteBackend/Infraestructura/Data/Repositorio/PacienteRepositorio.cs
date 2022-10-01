using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class PacienteRepositorio : Repositorio<Paciente>, IPacienteRepositorio
    {
        private readonly ApplicationDbContext _db;
        public PacienteRepositorio(ApplicationDbContext db) :base(db)
        {
            _db = db;

        }
        public void Actualizar(Paciente paciente)
        {
            var pacienteDB = _db.TbPaciente.FirstOrDefault(m=>m.Id==paciente.Id);

            if(paciente != null)
            {
                pacienteDB.Apellidos = paciente.Apellidos;
                pacienteDB.Direccion = paciente.Direccion;
                pacienteDB.Medico = paciente.Medico;
                pacienteDB.MotivoConsulta = paciente.MotivoConsulta;
                pacienteDB.Nombres = paciente.MotivoConsulta;
                pacienteDB.HospitalId = paciente.HospitalId;

                _db.SaveChanges();
            }
        }
    }
}
using Infraestructura.Data.IRepositorio;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {

        private readonly ApplicationDbContext _db;

         public IHospitalRepositorio Hospital  {get; private set;}

        public IPacienteRepositorio Paciente {get; private set;}

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Hospital = new HospitalRepositorio(db);
            Paciente = new PacienteRepositorio(db);
        }
       
        public void Dispose()
        {
           _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
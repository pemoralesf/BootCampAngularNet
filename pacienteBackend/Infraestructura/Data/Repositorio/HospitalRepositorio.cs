using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace Infraestructura.Data.Repositorio
{
    public class HospitalRepositorio : Repositorio<Hospital>, IHospitalRepositorio
    {
        private readonly ApplicationDbContext _db;
        public HospitalRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
        public void Actualizar(Hospital hospital)
        {
            var hospitalDB = _db.TbHospital.FirstOrDefault(p=> p.Id==hospital.Id);
            if(hospitalDB != null)
            {
                hospitalDB.NombreHospital = hospital.NombreHospital;
                hospitalDB.Direccion = hospital.Direccion;
                hospitalDB.Correo = hospital.Correo;
                hospitalDB.Telefono = hospital.Telefono;
                _db.SaveChanges();

            }
        }
    }
}
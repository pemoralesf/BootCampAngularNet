using Core.Entidades;
using Infraestructura.Data.config;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new HospitalConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
        } 

        public DbSet<Hospital> TbHospital { get; set; }

        public DbSet<Paciente> TbPaciente { get; set; }
    }
}
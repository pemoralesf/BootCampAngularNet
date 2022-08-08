using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Data.config
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Nombres).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Apellidos).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Direccion).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Medico).IsRequired().HasMaxLength(150);
            builder.Property(p => p.MotivoConsulta).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Telefono).IsRequired().HasMaxLength(100);
            builder.Property(p => p.HospitalId).IsRequired();

            //  Relaciones

            builder.HasOne(p => p.Hospital).WithMany()
                        .HasForeignKey (p => p.HospitalId);
        }
    }
}
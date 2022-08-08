using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Data.config
{
    public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
           builder.Property(h => h.Id).IsRequired();
           builder.Property(h => h.Correo).IsRequired().HasMaxLength(100);
           builder.Property(h => h.Direccion).IsRequired().HasMaxLength(150);
           builder.Property(h => h.NombreHospital).IsRequired().HasMaxLength(100);
           builder.Property(h => h.Telefono).IsRequired().HasMaxLength(50);
        }
    }
}
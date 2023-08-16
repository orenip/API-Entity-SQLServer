using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace IntroduccionAEFCore.Entidades.Configuraciones
{
    public class ActorConfig :IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder) 
        {
            //modelBuilder.Entity<Actor>().Property(x => x.Nombre).HasMaxLength(150);
            //Cambiamos modelBuilder.Entity<Actor>(). del DBContext a builder
            builder.Property(x => x.FechaNacimiento).HasColumnType("date");
            builder.Property(x => x.Fortuna).HasPrecision(18, 2);
        }
    }
}

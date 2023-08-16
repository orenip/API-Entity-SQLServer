using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntroduccionAEFCore.Entidades.Configuraciones
{
    public class GeneroConfig: IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            //Se debe poner una que no exista ya en BD--Ejecutamos Migracion DatosGeneros
            var cienciaFiccion = new Genero { Id = 5, Nombre = "Ciencia Ficción" }; 
            var animacion = new Genero { Id = 6, Nombre = "Animación" };
            builder.HasData(cienciaFiccion, animacion);


            //CREAR INDICES, se agrega migracion --> Debemos ir al Controller despues pàra modificar.
            builder.HasIndex(x => x.Nombre).IsUnique();
        }
    }
}

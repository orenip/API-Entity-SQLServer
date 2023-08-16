using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntroduccionAEFCore.Entidades.Configuraciones
{
    public class PeliculaActorConfig:IEntityTypeConfiguration<PeliculaActor>
    {
        public void Configure(EntityTypeBuilder<PeliculaActor> builder)
        {
            //Indica que queremos una compuesta -->En pelicula hacemos un listado.
            builder.HasKey(x => new {x.ActorId,x.PeliculaId});
        }
    }
}

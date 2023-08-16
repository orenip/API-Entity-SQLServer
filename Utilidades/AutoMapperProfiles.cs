using AutoMapper;
using IntroduccionAEFCore.DTOs;
using IntroduccionAEFCore.Entidades;

namespace IntroduccionAEFCore.Utilidades
{
    //Configuración de AutoMapper y añadir a Program.cs
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GeneroCreacionDTO, Genero>();
            //MAPEAMOS TAMBIEN RESTO DE TABLAS
            CreateMap<ActorCreacionDTO, Actor>();
            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Actor, ActorDTO>();

            //En este caso debemos enseñar a AUTOMAPPER tratar si los campos no corresponden.
            //Se llama proyeccion.
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(ent=>ent.Generos, dto => 
                dto.MapFrom(x=> x.Generos.Select(id=>new Genero { Id = id })));

            CreateMap<PeliculaActorCreacionDTO, PeliculaActor>();

        }
    }
}

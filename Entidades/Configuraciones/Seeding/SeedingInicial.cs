using Microsoft.EntityFrameworkCore;
using System;

namespace IntroduccionAEFCore.Entidades.Configuraciones.Seeding
{
    public class SeedingInicial
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //Añadir Seeder ACTORES
            var samuelLJackson = new Actor() 
            {
                Id = 2, Nombre = "Samuel L. Jackson", 
                FechaNacimiento = new DateTime(1948, 12, 21), 
                Fortuna = 15000 
            };
            var RobertDowneyJunior = new Actor() 
            { 
                Id = 3, Nombre = "Robert Downey Jr.", 
                FechaNacimiento = new DateTime(1965, 4, 4), 
                Fortuna = 18000 
            };

            modelBuilder.Entity<Actor>().HasData(samuelLJackson,RobertDowneyJunior);

            //SEEDER PELICULAS
            var avenger = new Pelicula()
            {
                Id = 2,
                Titulo = "Avengers Endgame",
                FechaEstreno = new DateTime(2019, 4, 22),
            };
            var spiderManNWH = new Pelicula()
            {
                Id = 3,
                Titulo = "Spider-Man: No Way Home",
                FechaEstreno = new DateTime(2019, 4, 22),
            };
            var spiderManSpiderVerse2 = new Pelicula()
            {
                Id = 4,
                Titulo = "Spider-Man: Across the Spider-Verse (Part One)",
                FechaEstreno = new DateTime(2021, 12, 13),
            };

            modelBuilder.Entity<Pelicula>().HasData(avenger, spiderManNWH, spiderManSpiderVerse2);

            //SEDDER COMENTARIOS
            var comentarioAvenger = new Comentario()
            {
                Id = 2,
                Recomendar = true,
                Contenido = "Muy buena!!",
                PeliculaId = avenger.Id,
            };
            var comentarioAvenger2 = new Comentario()
            {
                Id = 3,
                Recomendar = true,
                Contenido = "Dura dura",
                PeliculaId = avenger.Id,
            };
            var comentarioNWH = new Comentario()
            {
                Id = 4,
                Recomendar = false,
                Contenido = "no debieron hacer eso...",
                PeliculaId = spiderManNWH.Id,
            };

            modelBuilder.Entity<Comentario>().HasData(comentarioAvenger, comentarioAvenger2, comentarioNWH);

            //muchos a muchos con salto (AVANZADO) Insertar datos en la relacion N-M que no se utiliza la intermedia.
            var tablaGeneroPelicula = "GeneroPelicula";
            var generoIdPropiedad = "GenerosId";
            var peliculaIdPropiedad = "PeliculasId";

            var cienciaFiccion = 5;
            var animacion = 6;


            modelBuilder.Entity(tablaGeneroPelicula).HasData(
                new Dictionary<string, object>
                {
                    [generoIdPropiedad] = cienciaFiccion,
                    [peliculaIdPropiedad] = avenger.Id
                },
                new Dictionary<string, object>
                {
                    [generoIdPropiedad] = cienciaFiccion,
                    [peliculaIdPropiedad] = spiderManNWH.Id
                }, 
                new Dictionary<string, object>
                {
                    [generoIdPropiedad] = animacion,
                    [peliculaIdPropiedad] = spiderManSpiderVerse2.Id
                });

            //Muchos a muchos n-M sin salto
            var SamuelJacksonSpiderManNWH = new PeliculaActor
            {
                ActorId = samuelLJackson.Id,
                PeliculaId = spiderManNWH.Id,
                Orden = 1,
                Personaje = "Nick Fury"
            };
            var SamuelJacksonAvenger = new PeliculaActor
            {
                ActorId = samuelLJackson.Id,
                PeliculaId = avenger.Id,
                Orden = 2,
                Personaje = "Nick Fury"
            };
            var roberDowneyJuniorAvengers = new PeliculaActor
            {
                ActorId = RobertDowneyJunior.Id,
                PeliculaId = avenger.Id,
                Orden = 1,
                Personaje = "Iron Man"
            };

            modelBuilder.Entity<PeliculaActor>()
                .HasData(
                SamuelJacksonSpiderManNWH, 
                SamuelJacksonAvenger, 
                roberDowneyJuniorAvengers);

            //Realizamos comprobaciones de compilazion y añadimos migracion DataSeederInicial

        }
    }
}

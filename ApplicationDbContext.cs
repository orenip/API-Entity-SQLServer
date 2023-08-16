using IntroduccionAEFCore.Entidades;
using IntroduccionAEFCore.Entidades.Configuraciones.Seeding;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IntroduccionAEFCore
{
    // 1- Creamos la clase que hereda de DbContext.
    //Es donde configuramos todo, tablas a crear etc.. PIEZA CENTRAL
    public class ApplicationDbContext : DbContext
    {
        //CTRL+. para generar constructor (Con OPTION PARA ENVIAR EJ CONEXION)->Program.cs
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //4-Apifluente
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //!ATENCION! SE LLEVA AL ARCHIVO DE CONFIG DENTRO DE ENTIDADES PARA MEJOR MANEJO
            //Añadimos lo que queremos configurar indicando el campo que queramos que sea:
            //modelBuilder.Entity<Genero>().HasKey(x => x.Id); //Es innecesario es misma PK
            //Para poner el campo de maximo 150 caracteres.//No es necesario si esta en ConfigureConvention por defecto.
            //modelBuilder.Entity<Genero>().Property(x=> x.Nombre).HasMaxLength(150);
            //modelBuilder.Entity<Actor>().Property(x => x.Nombre).HasMaxLength(150);
            //modelBuilder.Entity<Actor>().Property(x => x.FechaNacimiento).HasColumnType("date");
            //modelBuilder.Entity<Actor>().Property(x => x.Fortuna).HasPrecision(5,2);
            //modelBuilder.Entity<Pelicula>().Property(x => x.Titulo).HasMaxLength(150);
            //modelBuilder.Entity<Pelicula>().Property(x => x.FechaEstreno).HasColumnType("date");
            //modelBuilder.Entity<Comentario>().Property(x => x.Contenido).HasMaxLength(500);

            //Para aplicar las CONFIG hay que poner:
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Para crear SEEDER, se le agrega dentro la data que se quiera asignar...
            //Pero para que se quede mas limpio se puede meter en archivos-> Entidades->Config
            //modelBuilder.Entity<Genero>().HasData()
            SeedingInicial.Seed(modelBuilder);
        }
        //Para configurar convenciones en plan datos predeterminados para x campos.
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(150);
        }
        //3-Creamos la tabla: -> Ejecutamos migraciones con codigo Add-Migration x / update-database
        public DbSet<Genero> Generos => Set<Genero>();
        public DbSet<Actor> Actores=> Set<Actor>();
        public DbSet<Pelicula> Peliculas => Set<Pelicula>();
        public DbSet<Comentario>Comentarios => Set<Comentario>();
        public DbSet<PeliculaActor>PeliculasActores=> Set<PeliculaActor>();
    }
}

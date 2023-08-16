using System.ComponentModel.DataAnnotations;

namespace IntroduccionAEFCore.Entidades
{
    public class Genero
    {
        //Podemos poner ? para indicar que puede ser nulo, y para perdonar el nulo
        //lo inicializamos a =null!; 
        public int Id { get; set; }
        //Metodo de Anotacion de datos, realizado tambien en Apifluente
        //[StringLength(maximumLength: 150)]
        public string Nombre { get; set; } = null!;

        public HashSet<Pelicula> Peliculas { get; set; } = new HashSet<Pelicula>();
    }
}

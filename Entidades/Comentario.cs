namespace IntroduccionAEFCore.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        //Campo contenido puede ser Null
        public string? Contenido { get; set; }
        public bool Recomendar { get; set; }
        //Añadimos para relacionar con tabla de Pelicula el campo que hace referencia.
        public int PeliculaId { get; set; }
        //Y la propiedad de navegacion --> Debemos ir a la otra entidad :Pelicula
        public Pelicula Pelicula { get; set; } = null!;

    }
}

namespace IntroduccionAEFCore.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public bool EnCines { get; set; }   
        public DateTime FechaEstreno { get; set; }
        //Propiedad de navegacion, Si es 1 a Muchos se puede usar HashSet indica que son muchos comentarios
        //HashSet no deja ordenar, pero es mas rapido.
        public HashSet<Comentario> Comentarios { get; set; } = new HashSet<Comentario>();
        public HashSet<Genero> Generos { get; set; }= new HashSet<Genero>();
        public List<PeliculaActor> PeliculasActores { get; set;}= new List<PeliculaActor>();

    }
}

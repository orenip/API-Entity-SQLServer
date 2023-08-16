using System.ComponentModel.DataAnnotations;

namespace IntroduccionAEFCore.DTOs
{
    public class GeneroCreacionDTO
    {
        //Permite hacer una validacion automatica, por si un usuario no cumple con la condición (FRONT)
        [StringLength(maximumLength: 150)]
        public string Nombre { get; set; } = null!;
    }
}

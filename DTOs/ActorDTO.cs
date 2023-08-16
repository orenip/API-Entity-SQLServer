namespace IntroduccionAEFCore.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
       
        //Con Automapper podemos meter tantos campos como queramos mostrar y automaticamente los mapea
        //public decimal Fortuna { get; set; }    
    }
}

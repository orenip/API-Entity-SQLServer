using AutoMapper;
using IntroduccionAEFCore.DTOs;
using IntroduccionAEFCore.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroduccionAEFCore.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PeliculasController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;  
        }

        [HttpPost]
        public async Task <ActionResult>Post (PeliculaCreacionDTO peliculaCreacionDTO)
        {
            //Se le añade esta condicion para dar seguimiento y corresponden.
            //Cuando es sin cambiar se le dice que no debe crear nuevo genero sino que ya son existente
            //Add es para que se inserte por ejemplo
            var pelicula = _mapper.Map<Pelicula>(peliculaCreacionDTO);
            if(pelicula.Generos is not null)
            {
                foreach(var genero in pelicula.Generos)
                {   //Indica que ese registro ya existe. Se hace porque estamos saltando la tabla intermedia.
                    _context.Entry(genero).State = EntityState.Unchanged;
                }
            }
            //En este caso si estamos accediendo a la tabla intermedia.
            if(pelicula.PeliculasActores is not null)
            {
                for(int i=0; i<pelicula.PeliculasActores.Count;i++)
                {
                    pelicula.PeliculasActores[i].Orden = i + 1;
                }
            }

            _context.Add(pelicula);
            await _context.SaveChangesAsync();
            return Ok();
        }
        //Traer datos de otras tablas (Include)
        //Cuando hay referencias circulares se pueden manejar desde clase program.
        //Incluimos datos de la tabla intermedia y tablas que hacen referencias a otras.
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pelicula>>Get (int id)
        {
            var pelicula= await _context.Peliculas
                .Include(x=>x.Comentarios)
                .Include(x=>x.Generos)
                .Include(x => x.PeliculasActores.OrderBy(y=>y.Orden))
                    .ThenInclude(x=>x.Actor)
                .FirstOrDefaultAsync(x=>x.Id==id);
            if (pelicula is null)
            {
                return NotFound();
            }

            return pelicula;

        }

        [HttpGet("select/{id:int}")]
        public async Task<ActionResult> GetSelect(int id)
        {
            var pelicula = await _context.Peliculas
                .Select(x => new
                {
                    x.Id,
                    x.Titulo,
                    Generos = x.Generos.Select(x => x.Nombre).ToList(),
                    Actores = x.PeliculasActores.OrderBy(x => x.Orden).Select(y => new
                    {
                        Id = y.ActorId,
                        y.Actor.Nombre,
                        y.Personaje
                    }),
                    CantidadComentarios=x.Comentarios.Count()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pelicula is null)
            {
                return NotFound();
            }

            return Ok(pelicula);

        }

        //MODELO BORRADO  MODERNO (Relacionado)
        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            //Verifica y borra en una sola Query
            var filasAlteradas = await _context.Peliculas
                .Where(x => x.Id == id).ExecuteDeleteAsync();

            if (filasAlteradas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

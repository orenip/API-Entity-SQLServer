using AutoMapper;
using IntroduccionAEFCore.DTOs;
using IntroduccionAEFCore.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroduccionAEFCore.Controllers
{
    //Añadimos el control de la API y la RUTA
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        //Inicializamos como un campo.
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        //Añadir el AUTOMAPPER para poder mapear la clase con la entidad.
        public GenerosController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        //Agregar campos a la tabla Generos
        [HttpPost]
        public async Task<ActionResult> Post(GeneroCreacionDTO generoCreacion)
        {
            //Para mapear de manera manual: NO ES NECESARIO SI TIENE EL AUTOMAPPER
            //var genero = new Genero
            //{
            //    Nombre = generoCreacion.Nombre
            //};

            //Verificacion si se añade Indice y ya existe.
            var yaExisteGeneroConEsteNombre= await _context.Generos
                .AnyAsync(x=>x.Nombre== generoCreacion.Nombre);
            if (yaExisteGeneroConEsteNombre)
            {
                return BadRequest("Ya existe un género con el nombre " + generoCreacion.Nombre);
            }
            //Utilizar de esta manera con AUTOMAPPER (Incluido en constructor
            var genero = _mapper.Map<Genero>(generoCreacion);

            _context.Add(genero);
            await _context.SaveChangesAsync();
            return Ok();
        }
        //Agregar varios generos a la vez.
        [HttpPost("varios")]
        public async Task<ActionResult> Post(GeneroCreacionDTO[] generosCreacionDTO)
        {
            //Añadimos AddRange para un Array de datos.
            var generos = _mapper.Map<Genero[]>(generosCreacionDTO);
            _context.AddRange(generos);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Visualizar datos: //TODOS LOS REGISTROS DE LA TABLA GENEROS
        //MODELO CONECTADO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            return await _context.Generos.ToListAsync();
        }

        [HttpPut("{id:int}/nombre2")]
        public async Task<ActionResult> Put(int id)
        {
            var genero= await _context.Generos.FirstOrDefaultAsync(x=>x.Id==id);
            if( genero is null )
            {
                return NotFound();
            }
            genero.Nombre = genero.Nombre + "2";
            await _context.SaveChangesAsync();
            return Ok();
        }

        //MODELO DESCONECTADO
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = _mapper.Map<Genero>(generoCreacionDTO);
            genero.Id = id;

            _context.Update(genero);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //MODELO BORRADO  MODERNO
        [HttpDelete("{id:int}/moderna")]
        public async Task <ActionResult> Delete(int id)
        {
            //Verifica y borra en una sola Query
            var filasAlteradas = await _context.Generos.Where(x=>x.Id == id).ExecuteDeleteAsync();

            if (filasAlteradas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        //MODELO VIEJO (Si queremos obtener algun dato del genero antes de borrarlo..)
        [HttpDelete("{id:int}/anterior")]
        public async Task<ActionResult> DeleteAnterio(int id)
        {
            //Query de que existe y despues...
            var genero = await _context.Generos.FirstOrDefaultAsync(x=>x.Id==id);

            if (genero is null)
            {
                return NotFound();
            }
            //Query de borrado.
            _context.Remove(genero);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}

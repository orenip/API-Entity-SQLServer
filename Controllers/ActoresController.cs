using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntroduccionAEFCore.DTOs;
using IntroduccionAEFCore.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

namespace IntroduccionAEFCore.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public ActoresController(ApplicationDbContext context, IMapper mapper) 
        {
            this._context=context;
            this._mapper=mapper;
        }

        //Una vez creada en AUTOMAPPERPROFILES LO MAPEAMOS Y LISTO
        [HttpPost]
        public async Task<ActionResult> Post(ActorCreacionDTO actorCreacionDTO)
        {
            var actor = _mapper.Map<Actor>(actorCreacionDTO);
            _context.Add(actor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()
        {
            //Con OrderBy ordenas alfabeticamente o OrderByDescending
            return await _context.Actores.OrderByDescending(x=>x.FechaNacimiento).ToListAsync();
        }

        //Filtrar por nombre
        [HttpGet("nombre")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(string nombre)
        {
            //Version 1 es Igualdad y quizas no es lo mejor para STRING.
            //Se puede ordenar tambien por nombre y dentro por fecha de nacimiento por ejemplo
            return await _context.Actores
                .Where(x=> x.Nombre== nombre)
                .OrderBy(x=>x.Nombre)
                    //.ThenBy(x=>x.FechaNacimiento)
                    .ThenByDescending(x => x.FechaNacimiento)
                .ToListAsync();
        }

        [HttpGet("nombre/v2")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetV2(string nombre)
        {
            //Version 2 que contenga parte del nombre
            return await _context.Actores.Where(x => x.Nombre.Contains(nombre)).ToListAsync();
        }

        [HttpGet("fechaNacimiento/rango")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(DateTime inicio,DateTime fin)
        {
            //Version 3 en un rango de fechas
            return await _context.Actores
                .Where(x => x.FechaNacimiento>=inicio &&x.FechaNacimiento<=fin)
                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Actor>> Get(int id)
        {
            //Version 4 solo nos retorna un actor, primero o por defecto. (null)
            var actor=await _context.Actores
                .FirstOrDefaultAsync(x=>x.Id==id);
            if(actor is null)
            {
                return NotFound();
            }
            return Ok(actor);
        }

        [HttpGet("idynombre")]
        public async Task<ActionResult> Getidynombre()
        {
            var actores= await _context.Actores
                .Select(x => new {x.Id, x.Nombre})
                .ToListAsync();
            return Ok(actores);
        }

        [HttpGet("idynombreDTO")]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GetidynombreDTO()
        {
            //Mapeo manual
            //return await _context
            //    .Actores.Select(x => new ActorDTO {Id= x.Id,Nombre= x.Nombre})
            //    .ToListAsync();

            //Mapeo Automapper
            return await _context.Actores
                .ProjectTo<ActorDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }


    }
}

using AutoMapper;
using IntroduccionAEFCore.DTOs;
using IntroduccionAEFCore.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IntroduccionAEFCore.Controllers
{
    [ApiController]
    [Route("api/pelicula/{peliculaId:int}/comentarios")]
    public class ComentariosController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;  
        }

        [HttpPost]
        public async Task<ActionResult>Post (int peliculaId,ComentarioCreacionDTO comentarioCreacionDTO)
        {
            var comentario = _mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.PeliculaId= peliculaId;
            _context.Add(comentario);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

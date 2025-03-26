using Galeria.API.Controllers.BaseGeneric;
using Galeria.Application.Interfaces.Categorias;
using Galeria.Application.Interfaces.Comentarios;
using Galeria.Domain.DTO.Comentarios;
using Galeria.Domain.Entities.Comentarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Galeria.API.Controllers.Comentarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : BaseController<Comentario,ComentarioDTO>
    {
        private readonly IComentarioService _service;
        public ComentarioController(IComentarioService service) : base(service)
        {
            _service = service;
        }
        [HttpGet("datos")]
        public async Task<ActionResult<List<ComentarioDatosDTO>>> ComentarioDatos(
        [FromQuery] int? page = null, [FromQuery] int? limit = null,
        [FromQuery] string? orderBy = null, [FromQuery] string? orderDirection = "asc",
        [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null,
        [FromQuery] string? filterField = null, [FromQuery] string? filterValue = null)
        {
            var result = await _service.GetComentarioDatos(page, limit, orderBy, orderDirection, startDate, endDate, filterField, filterValue);
            return Ok(result);
        }
    }
    
}

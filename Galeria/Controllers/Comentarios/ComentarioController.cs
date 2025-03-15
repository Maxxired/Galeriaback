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
    }
}

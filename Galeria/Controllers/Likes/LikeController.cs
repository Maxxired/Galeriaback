using Galeria.API.Controllers.BaseGeneric;
using Galeria.Application.Interfaces.Likes;
using Galeria.Application.Interfaces.Usuarios.Artistas;
using Galeria.Domain.DTO.Likes;
using Galeria.Domain.Entities.Likes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Galeria.API.Controllers.Likes
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : BaseController<Like,LikeDTO>
    {
        private readonly ILikeService _service;
        public LikeController(ILikeService service) : base(service)
        {
            _service = service;
        }
    }
}

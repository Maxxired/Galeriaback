using Galeria.API.Controllers.BaseGeneric;
using Galeria.Application.Interfaces.Likes;
using Galeria.Application.Interfaces.Obras;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Obras;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Galeria.API.Controllers.Obras
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraController : BaseController<Obra,ObraDTO>
    {
        private readonly IObraService _service;
        public ObraController(IObraService service) : base(service)
        {
            _service = service;
        }
    }
}

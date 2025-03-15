using Galeria.API.Controllers.BaseGeneric;
using Galeria.Application.Interfaces.Comentarios;
using Galeria.Application.Interfaces.Exposiciones;
using Galeria.Domain.DTO.Exposiciones;
using Galeria.Domain.Entities.Exposiciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Galeria.API.Controllers.Exposiciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExposicionController : BaseController<Exposicion,ExposicionDTO>
    {
        private readonly IExposicionService _service;
        public ExposicionController(IExposicionService service) : base(service)
        {
            _service = service;
        }
    }
}

using Galeria.Application.Interfaces.Logs;
using Galeria.Domain.DTO.Logs;
using Galeria.Domain.Entities.Logs;
using Galeria.API.Controllers.BaseGeneric;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galeria.API.Controllers.Logs
{
    [Route("api/[controller]")]
    [Authorize(Roles="Admin")]
    [ApiController]
    public class LogActionController : BaseController<LogAction, LogActionDTO>
    {
        private readonly ILogActionService _service;
        public LogActionController(ILogActionService service) : base(service)
        {
            _service = service;
        }
    }
}

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
    public class LogErrorController : BaseController<LogError, LogErrorDTO>
    {
        private readonly ILogErrorService _service;
        public LogErrorController(ILogErrorService service) : base(service)
        {
            _service = service;
        }
    }
}

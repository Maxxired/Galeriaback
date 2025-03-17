using AutoMapper;
using Galeria.Application.Interfaces.Logs;
using Galeria.Application.Services.Base;
using Galeria.Domain.DTO.Logs;
using Galeria.Domain.Entities.Logs;
using Galeria.Infraestructure.Interfaces.Log;

namespace Galeria.Application.Services.Logs
{
    public class LogErrorService : ServiceBase<LogError, LogErrorDTO>, ILogErrorService
    {
        private readonly IMapper _mapper;
        private readonly ILogErrorRepository _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        public LogErrorService(IMapper mapper, ILogErrorRepository repository, ILogActionRepository logAction, ILogErrorRepository logError) : base(mapper,repository,logAction,logError)
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
        }
    }
}

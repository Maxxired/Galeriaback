using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Galeria.Application.Interfaces.Obras;
using Galeria.Application.Services.Base;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Obras;
using Galeria.Infraestructure.Interfaces.Log;
using Galeria.Infraestructure.Interfaces.Obras;

namespace Galeria.Application.Services.Obras
{
    public class ObraEnExposicionService: ServiceBase<ObraEnExposicion, ObraEnExposicionDTO>, IObraEnExposicionService
    {
        private readonly IMapper _mapper;
        private readonly IObraEnExposicionRepository _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        public ObraEnExposicionService(IObraEnExposicionRepository repository, IMapper mapper, ILogActionRepository logAction, ILogErrorRepository logError) : base(mapper, repository, logAction, logError)
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
        }
    }
}

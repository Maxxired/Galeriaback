using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Galeria.Application.Interfaces.Exposiciones;
using Galeria.Application.Services.Base;
using Galeria.Domain.DTO.Exposiciones;
using Galeria.Domain.Entities.Exposiciones;
using Galeria.Infraestructure.Interfaces.Comentarios;
using Galeria.Infraestructure.Interfaces.Esposiciones;
using Galeria.Infraestructure.Interfaces.Log;

namespace Galeria.Application.Services.Exposiciones
{
    public class ExposicionService : ServiceBase<Exposicion,ExposicionDTO>,IExposicionService
    {
        private readonly IMapper _mapper;
        private readonly IExposicionRepository _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        public ExposicionService(IExposicionRepository repository, IMapper mapper, ILogActionRepository logAction, ILogErrorRepository logError) : base(mapper, repository, logAction, logError)
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
        }
    }
}

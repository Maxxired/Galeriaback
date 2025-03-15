using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Galeria.Application.Interfaces.Comentarios;
using Galeria.Application.Services.Base;
using Galeria.Domain.DTO.Comentarios;
using Galeria.Domain.Entities.Comentarios;
using Galeria.Infraestructure.Interfaces.Comentarios;
using Galeria.Infraestructure.Interfaces.Log;

namespace Galeria.Application.Services.Comentarios
{
    public class ComentarioService : ServiceBase<Comentario,ComentarioDTO>,IComentarioService
    {
        private readonly IMapper _mapper;
        private readonly IComentarioRepository _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        public ComentarioService(IComentarioRepository repository, IMapper mapper, ILogActionRepository logAction, ILogErrorRepository logError) : base(mapper, repository, logAction, logError)
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
        }
    }
}

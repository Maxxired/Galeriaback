using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Galeria.Application.Interfaces.Likes;
using Galeria.Application.Services.Base;
using Galeria.Domain.DTO.Likes;
using Galeria.Domain.Entities.Likes;
using Galeria.Infraestructure.Interfaces.Esposiciones;
using Galeria.Infraestructure.Interfaces.Likes;
using Galeria.Infraestructure.Interfaces.Log;

namespace Galeria.Application.Services.Likes
{
    public class LikeService: ServiceBase<Like,LikeDTO>, ILikeService
    {
        private readonly IMapper _mapper;
        private readonly ILikeRepository _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        public LikeService(ILikeRepository repository, IMapper mapper, ILogActionRepository logAction, ILogErrorRepository logError) : base(mapper, repository, logAction, logError)
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
        }
    }
}

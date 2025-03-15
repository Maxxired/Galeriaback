using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Galeria.Application.Interfaces.Usuarios.Artistas;
using Galeria.Application.Services.Base;
using Galeria.Domain.DTO.Usuarios.Artistas;
using Galeria.Domain.Entities.Usuarios.Artistas;
using Galeria.Infraestructure.Interfaces.Likes;
using Galeria.Infraestructure.Interfaces.Log;
using Galeria.Infraestructure.Interfaces.Usuarios.Artistas;

namespace Galeria.Application.Services.Usuarios.Artistas
{
    public class ArtistaService : ServiceBase<Artista,ArtistaDTO>,IArtistaService
    {
        private readonly IMapper _mapper;
        private readonly IArtistaRepository _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        public ArtistaService(IArtistaRepository repository, IMapper mapper, ILogActionRepository logAction, ILogErrorRepository logError) : base(mapper, repository, logAction, logError)
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Galeria.Application.Interfaces.Categorias;
using Galeria.Application.Services.Base;
using Galeria.Domain.DTO.Categorias;
using Galeria.Domain.Entities.Categorias;
using Galeria.Infraestructure.Interfaces.Categorias;
using Galeria.Infraestructure.Interfaces.Log;
using Galeria.Infraestructure.Interfaces.Usuarios.Personas;
using Microsoft.AspNetCore.Hosting;

namespace Galeria.Application.Services.Categorias
{
    public class CategoriaService : ServiceBase<Categoria, CategoriaDTO>, ICategoriaService
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        public CategoriaService(ICategoriaRepository repository, IMapper mapper, ILogActionRepository logAction, ILogErrorRepository logError) : base(mapper, repository, logAction, logError)
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
        }
    }
}

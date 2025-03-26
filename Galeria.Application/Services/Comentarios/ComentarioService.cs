using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Galeria.Application.Interfaces.Comentarios;
using Galeria.Application.Services.Base;
using Galeria.Domain.Common.ViewModels.Util;
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

        public async Task<ResponseHelper> GetComentarioDatos(
           int? page = null, int? limit = null,
           string? orderBy = null, string? orderDirection = "asc",
           DateTime? startDate = null, DateTime? endDate = null,
           string? filterField = null, string? filterValue = null)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var data = await _repository.GetComentarioDatos(page, limit, orderBy, orderDirection, startDate, endDate, filterField, filterValue);

                var items = data.Items;
                var total = data.Total;

                response.Success = true;
                response.Data = new
                {
                    Items = items,
                    Total = total
                };
            }
            catch (Exception e)
            {
                await LogError(e);
                response.Message = e.Message;
            }
            return response;
        }
    }
}

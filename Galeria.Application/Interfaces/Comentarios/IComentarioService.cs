using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Comentarios;
using Galeria.Domain.Entities.Comentarios;

namespace Galeria.Application.Interfaces.Comentarios
{
    public interface IComentarioService: IServiceBase<Comentario,ComentarioDTO>
    {
        Task<ResponseHelper> GetComentarioDatos(
           int? page = null, int? limit = null,
           string? orderBy = null, string? orderDirection = "asc",
           DateTime? startDate = null, DateTime? endDate = null,
           string? filterField = null, string? filterValue = null);
    }
}

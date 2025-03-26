using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.DTO.Comentarios;
using Galeria.Domain.Entities.Comentarios;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Interfaces.Comentarios
{
    public interface IComentarioRepository : IBaseRepository<Comentario>
    {
        Task<(List<ComentarioDatosDTO> Items, int Total)> GetComentarioDatos(
            int? page = null, int? limit = null,
            string? orderBy = null, string? orderDirection = "asc",
            DateTime? startDate = null, DateTime? endDate = null,
            string? filterField = null, string? filterValue = null);
    }
}

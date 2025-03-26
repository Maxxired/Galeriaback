using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Galeria.Domain.DTO.Comentarios;
using Galeria.Domain.Entities.Comentarios;
using Galeria.Infraestructure.Interfaces.Comentarios;
using Galeria.Infraestructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Galeria.Infraestructure.Repositories.Comentarios
{
    public class ComentarioRepository : BaseRepository<Comentario>, IComentarioRepository
    {
        private readonly ApplicationDbContext _context;
        public ComentarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<(List<ComentarioDatosDTO> Items, int Total)> GetComentarioDatos(
            int? page = null, int? limit = null,
            string? orderBy = null, string? orderDirection = "asc",
            DateTime? startDate = null, DateTime? endDate = null,
            string? filterField = null, string? filterValue = null)
        {

            var sql = new StringBuilder($"SELECT c.Texto as Texto, p.Nombres +' '+ p.Apellidos as PersonaNombre, c.FechaComentario as FechaComentario " +
                                        $"FROM Tbl_Comentarios as c " +
                                        $"INNER JOIN Tbl_Personas as p on c.idPersona = p.id " +
                                        $"WHERE c.IsDeleted = 0");

            var parameters = new DynamicParameters();

            if (startDate.HasValue)
            {
                sql.Append(" AND c.CreatedAt >= @StartDate");
                parameters.Add("StartDate", startDate.Value);
            }
            if (endDate.HasValue)
            {
                sql.Append(" AND c.CreatedAt <= @EndDate");
                parameters.Add("EndDate", endDate.Value);
            }

            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                sql.Append($" AND c.{filterField} LIKE @FilterValue");
                parameters.Add("FilterValue", $"%{filterValue}%");
            }


            var countSql = new StringBuilder($"SELECT COUNT(*) FROM Tbl_Comentarios as c " +
                                        $"INNER JOIN Tbl_Personas as p on c.idPersona = p.id " +
                                        $"WHERE c.IsDeleted = 0");

            if (startDate.HasValue)
            {
                countSql.Append(" AND CreatedAt >= @StartDate");
            }
            if (endDate.HasValue)
            {
                countSql.Append(" AND CreatedAt <= @EndDate");
            }
            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                countSql.Append($" AND {filterField} LIKE @FilterValue");
            }

            var total = await Context.Database.GetDbConnection().ExecuteScalarAsync<int>(countSql.ToString(), parameters);

            if (!string.IsNullOrEmpty(orderBy))
            {
                sql.Append($" ORDER BY c.{orderBy} {orderDirection}");
            }

            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "CreatedAt";
                sql.Append($" ORDER BY c.{orderBy} {orderDirection}");
            }

            if (page.HasValue && limit.HasValue)
            {
                int offset = (page.Value - 1) * limit.Value;
                sql.Append(" OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY");
                parameters.Add("Offset", offset);
                parameters.Add("Limit", limit.Value);
            }

            var items = await Context.Database.GetDbConnection().QueryAsync<ComentarioDatosDTO>(sql.ToString(), parameters);
            return (items.ToList(), total);
        }
    }
}

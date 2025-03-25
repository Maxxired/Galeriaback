using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Galeria.Domain.DTO.Likes;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Likes;
using Galeria.Infraestructure.Interfaces.Likes;
using Galeria.Infraestructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Galeria.Infraestructure.Repositories.Likes
{
    public class LikeRepository: ILikeRepository
    {
        private readonly ApplicationDbContext _context;
        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ToggleLikeAsync(int libroId, int usuarioId)
        {
            var usuario = await _context.Personas.FirstOrDefaultAsync(u => u.Id == usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            var likeExistente = await _context.Likes
                .FirstOrDefaultAsync(l => l.IdObra == libroId && l.IdPersona == usuario.Id);

            if (likeExistente != null)
            {
                _context.Likes.Remove(likeExistente);
                await _context.SaveChangesAsync();
                return false;
            }

            var nuevoLike = new Like
            {
                IdPersona = usuario.Id,
                IdObra = libroId,
                FechaLike = DateTime.UtcNow
            };

            _context.Likes.Add(nuevoLike);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleLikeByUserAsync(int libroId, string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var aspNetUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (string.IsNullOrEmpty(aspNetUserId))
            {
                throw new UnauthorizedAccessException("Token inválido o no contiene el ID de usuario.");
            }

            var usuario = await _context.Personas.FirstOrDefaultAsync(u => u.IdApplicationUser == aspNetUserId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            var likeExistente = await _context.Likes
                .FirstOrDefaultAsync(l => l.IdObra == libroId && l.IdPersona == usuario.Id);

            if (likeExistente != null)
            {
                _context.Likes.Remove(likeExistente);
                await _context.SaveChangesAsync();
                return false;
            }

            var nuevoLike = new Like
            {
                IdPersona = usuario.Id,
                IdObra = libroId,
                FechaLike = DateTime.UtcNow
            };

            _context.Likes.Add(nuevoLike);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<LikesDTO> GetLibroLikesInfoAsync(int libroId)
        {
            var likes = await _context.Likes
                .Where(l => l.IdObra == libroId)
                .Include(l => l.Persona)
                .ToListAsync();

            return new LikesDTO
            {
                IdObra = libroId,
                TotalLikes = likes.Count,
                UsuariosQueDieronLike = likes.Select(l => l.Persona.Nombres).ToList()
            };
        }
        public async Task<List<LibroLikesDTO>> GetAllLikesInfoAsync()
        {
            var likesGrouped = await _context.Likes
                .Include(l => l.Persona)
                .Include(l => l.Obra)
                .GroupBy(l => l.IdObra)
                .Select(g => new LibroLikesDTO
                {
                    LibroId = g.Key,
                    TituloLibro = g.First().Obra.Titulo,
                    TotalLikes = g.Count(),
                    UsuariosQueDieronLike = g.Select(l => l.Persona.Nombres).ToList()
                })
                .ToListAsync();

            return likesGrouped;
        }
        public async Task<(List<ObraLikesDTO> Items, int Total)> GetAllLikesFilterAsync(
      int? page = null, int? limit = null,
      string? orderBy = null, string? orderDirection = "asc",
      DateTime? startDate = null, DateTime? endDate = null,
      string? filterField = null, string? filterValue = null,
      string? relationField = null, int? relationId = null)
        {
            var sql = new StringBuilder(@"
                SELECT 
                    l.IdObra AS LibroId, 
                    o.Titulo AS TituloLibro, 
                    COUNT(l.IdPersona) AS TotalLikes, 
                    STRING_AGG(p.Nombres, ', ') AS UsuariosQueDieronLike
                FROM Tbl_Likes l
                JOIN Tbl_Personas p ON l.IdPersona = p.Id
                JOIN Tbl_Obras o ON l.IdObra = o.Id
                WHERE 1=1");

            var parameters = new DynamicParameters();

            if (startDate.HasValue)
            {
                sql.Append(" AND l.FechaLike >= @StartDate");
                parameters.Add("StartDate", startDate.Value);
            }
            if (endDate.HasValue)
            {
                sql.Append(" AND l.FechaLike <= @EndDate");
                parameters.Add("EndDate", endDate.Value);
            }

            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                sql.Append($" AND {filterField} LIKE @FilterValue");
                parameters.Add("FilterValue", $"%{filterValue}%");
            }

            if (!string.IsNullOrEmpty(relationField) && relationId.HasValue)
            {
                sql.Append($" AND {relationField} = @RelationId");
                parameters.Add("RelationId", relationId.Value);
            }

            sql.Append(" GROUP BY l.IdObra, o.Titulo");

            var countSql = new StringBuilder("SELECT COUNT(DISTINCT l.IdObra) FROM Tbl_Likes l WHERE 1=1");

            if (startDate.HasValue)
                countSql.Append(" AND l.FechaLike >= @StartDate");
            if (endDate.HasValue)
                countSql.Append(" AND l.FechaLike <= @EndDate");
            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
                countSql.Append($" AND {filterField} LIKE @FilterValue");
            if (!string.IsNullOrEmpty(relationField) && relationId.HasValue)
                countSql.Append($" AND {relationField} = @RelationId");

            var total = await _context.Database.GetDbConnection().ExecuteScalarAsync<int>(countSql.ToString(), parameters);

            if (!string.IsNullOrEmpty(orderBy))
            {
                sql.Append($" ORDER BY {orderBy} {orderDirection}");
            }
            else
            {
                sql.Append(" ORDER BY TotalLikes DESC");
            }

            if (page.HasValue && limit.HasValue)
            {
                int offset = (page.Value - 1) * limit.Value;
                sql.Append(" OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY");
                parameters.Add("Offset", offset);
                parameters.Add("Limit", limit.Value);
            }

            var likesGrouped = await _context.Database.GetDbConnection().QueryAsync<ObraLikesDTO>(sql.ToString(), parameters);
            return (likesGrouped.ToList(), total);
        }


        public async Task<List<ObrasDTO>> GetLikesByUserAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var aspNetUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (string.IsNullOrEmpty(aspNetUserId))
            {
                throw new UnauthorizedAccessException("Token inválido o no contiene el ID de usuario.");
            }

            var usuario = await _context.Personas.FirstOrDefaultAsync(u => u.IdApplicationUser == aspNetUserId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            var likedBooks = await _context.Likes
                .Where(l => l.IdPersona == usuario.Id)
                .Include(l => l.Obra)  // Cambiado de IdObra a Obra
                .ThenInclude(o => o.Artista)  // Necesario para acceder a Artista.Nombres
                .Select(l => new ObrasDTO
                {
                    Id = l.Obra.Id,
                    Titulo = l.Obra.Titulo,
                    Descripcion = l.Obra.Descripcion,
                    ArtistaNombre = l.Obra.Artista.Nombres,
                })
                .ToListAsync();

            return likedBooks;
        }

        public async Task<bool> DeleteAllLikesAsync()
        {

            var allLikes = await _context.Likes.ToListAsync();
            if (!allLikes.Any()) return false;

            _context.Likes.RemoveRange(allLikes);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

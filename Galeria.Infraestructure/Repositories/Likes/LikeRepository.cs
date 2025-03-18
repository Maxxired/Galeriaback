using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                .Include(l => l.IdObra)
                .Select(l => new ObrasDTO
                {
                    Id = l.Obra.Id,
                    Titulo = l.Obra.Titulo,
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

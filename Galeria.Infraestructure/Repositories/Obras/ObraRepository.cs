using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Obras;
using Galeria.Infraestructure.Interfaces.Obras;
using Galeria.Infraestructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Galeria.Infraestructure.Repositories.Obras
{
    public class ObraRepository : BaseRepository<Obra>, IObraRepository
    {
        private readonly ApplicationDbContext _context;
        public ObraRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> SubirImagenObra(int idObra, string url)
        {
            string sql = @"UPDATE Tbl_Obras SET ImagenUrl = @url WHERE Id = @idObra AND IsDeleted = 0";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(sql, new { idObra, url });
            return result;
        }

        public async Task<int> ActualizarImagenObra(int idObra, string url)
        {
            string sql = @"UPDATE Tbl_Obras SET ImagenUrl = @url WHERE Id = @idObra AND IsDeleted = 0";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(sql, new { idObra, url });
            return result;
        }

        public async Task<int> EliminarImagenObra(int idObra)
        {
            string sql = @"UPDATE Tbl_Obras SET ImagenUrl = NULL WHERE Id = @idObra AND IsDeleted = 0";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(sql, new { idObra });
            return result;
        }

        public async Task<bool> IsSlugTakenAsync(string slug)
        {
            return await _context.Obras.AnyAsync(o => o.Slug == slug);
        }
        public async Task<Obra> GetByIdAsync(int id)
        {
            return await _context.Obras.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<IEnumerable<ObraQueryDTO>> GetByTituloAsync(string titulo, int skip, int take)
        {
            string normalizedTitulo = NormalizeString(titulo);

            var libros = await _context.Obras
                .Include(l => l.Artista)
                .Include(l => l.ObrasCategorias).ThenInclude(le => le.Categoria)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var resultado = libros
                .Where(l => NormalizeString(l.Titulo).Contains(normalizedTitulo))
                .Select(l => new ObraQueryDTO
                {
                    Titulo = l.Titulo,
                    Slug = l.Slug,
                    Descripcion = l.Descripcion,
                    ArtistaNombre = l.Artista.Nombres,
                    Precio = l.Precio,
                    ImagenUrl = l.ImagenUrl,
                    Categorias = l.ObrasCategorias.Select(le => le.Categoria.Nombre).ToList()
                });

            return resultado;
        }
        public async Task<IEnumerable<ObraQueryDTO>> GetByEtiquetaNombreAsync(string etiqueta, int skip, int take)
        {
            string normalizedEtiqueta = NormalizeString(etiqueta);

            var libros = await _context.Obras
                .Include(l => l.Artista)
                .Include(l => l.ObrasCategorias).ThenInclude(le => le.Categoria)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var resultado = libros
                .Where(l => l.ObrasCategorias
                            .Any(le => NormalizeString(le.Categoria.Nombre).Contains(normalizedEtiqueta)))
                .Select(l => new ObraQueryDTO
                {
                    Titulo = l.Titulo,
                    Slug = l.Slug,
                    Descripcion = l.Descripcion,
                    ArtistaNombre = l.Artista.Nombres,
                    Precio = l.Precio,
                    ImagenUrl = l.ImagenUrl,
                    Categorias = l.ObrasCategorias.Select(le => le.Categoria.Nombre).ToList()
                });

            return resultado;
        }

        public async Task<IEnumerable<ObraQueryDTO>> GetByAutorNombreAsync(string autor, int skip, int take)
        {
            string normalizedAutor = NormalizeString(autor);

            var libros = await _context.Obras
                .Include(l => l.Artista)
                .Include(l => l.ObrasCategorias).ThenInclude(le => le.Categoria)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var resultado = libros
                .Where(l => NormalizeString(l.Artista.Nombres).Contains(normalizedAutor))
                .Select(l => new ObraQueryDTO
                {
                    Titulo = l.Titulo,
                    Slug = l.Slug,
                    Descripcion = l.Descripcion,
                    ArtistaNombre = l.Artista.Nombres,
                    Precio = l.Precio,
                    ImagenUrl = l.ImagenUrl,
                    Categorias = l.ObrasCategorias.Select(le => le.Categoria.Nombre).ToList()
                });

            return resultado;
        }


        public async Task<IEnumerable<ObraQueryDTO>> GetLibrosMasGustadosAsync()
        {
            return await _context.Obras
                .OrderByDescending(l => l.Likes.Count)
                .Include(l => l.Artista)
                .Include(l => l.ObrasCategorias).ThenInclude(le => le.Categoria)
                .Select(l => new ObraQueryDTO
                {
                    Titulo = l.Titulo,
                    Slug = l.Slug,
                    Descripcion = l.Descripcion,
                    ArtistaNombre = l.Artista.Nombres,
                    Precio = l.Precio,
                    ImagenUrl = l.ImagenUrl,
                    Categorias = l.ObrasCategorias.Select(le => le.Categoria.Nombre).ToList()
                })
                .Take(4)
                .ToListAsync();
        }

        public async Task<ObrasDTO> GetBySlugAsync(string slug)
        {
            var libro = await _context.Obras
                .Include(l => l.Artista)
                .Include(l => l.ObrasCategorias)
                    .ThenInclude(le => le.Categoria)
                .FirstOrDefaultAsync(l => l.Slug == slug);

            if (libro == null) return null;

            return new ObrasDTO
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Slug = libro.Slug,
                Descripcion = libro.Descripcion,
                ArtistaNombre = libro.Artista.Nombres,
                Categorias = libro.ObrasCategorias.Select(le => le.IdCategoria).ToList()
            };
        }

        public async Task<IEnumerable<ObrasDTO>> GetLibrosByAutorAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var aspNetUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (string.IsNullOrEmpty(aspNetUserId))
            {
                throw new UnauthorizedAccessException("Token inválido o no contiene el ID de usuario.");
            }

            var usuario = await _context.Artistas.FirstOrDefaultAsync(u => u.IdApplicationUser == aspNetUserId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            return await _context.Obras
                .Where(l => l.IdArtista == usuario.Id)
                .Include(l => l.Artista)
                .Include(l => l.ObrasCategorias)
                    .ThenInclude(le => le.Categoria)
                .Select(l => new ObrasDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Slug = l.Slug,
                    Descripcion = l.Descripcion,
                    IdArtista = l.IdArtista,
                    ArtistaNombre = l.Artista.Nombres,
                    Categorias = l.ObrasCategorias.Select(le => le.IdCategoria).ToList()
                })
                .ToListAsync();
        }

        private string NormalizeString(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            string normalized = input.Normalize(NormalizationForm.FormD);
            normalized = new string(normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
            normalized = normalized.Normalize(NormalizationForm.FormC);

            normalized = normalized.Replace(" ", "").ToLower();
            normalized = normalized.Replace("+", "").ToLower();

            normalized = normalized.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
                                   .Replace("ñ", "n").Replace("Á", "a").Replace("É", "e").Replace("Í", "i").Replace("Ó", "o")
                                   .Replace("Ú", "u").Replace("Ñ", "n");

            return normalized;
        }
    }
}

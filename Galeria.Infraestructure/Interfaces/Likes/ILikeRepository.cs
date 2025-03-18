using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.DTO.Likes;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Likes;
using Galeria.Infraestructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Galeria.Infraestructure.Interfaces.Likes
{
    public interface ILikeRepository
    {
        Task<bool> ToggleLikeAsync(int libroId, int usuarioId);
        Task<bool> ToggleLikeByUserAsync(int libroId, string token);
        Task<LikesDTO> GetLibroLikesInfoAsync(int libroId);
        Task<List<LibroLikesDTO>> GetAllLikesInfoAsync();
        Task<List<ObrasDTO>> GetLikesByUserAsync(string token);
        Task<bool> DeleteAllLikesAsync();
    }
}

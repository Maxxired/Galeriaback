using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Galeria.Application.Interfaces.Likes;
using Galeria.Application.Services.Base;
using Galeria.Domain.DTO.Likes;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Likes;
using Galeria.Infraestructure.Interfaces.Esposiciones;
using Galeria.Infraestructure.Interfaces.Likes;
using Galeria.Infraestructure.Interfaces.Log;
using Galeria.Infraestructure.Repositories.Likes;

namespace Galeria.Application.Services.Likes
{
    public class LikeService: ILikeService
    {
        private readonly IMapper _mapper;
        private readonly ILikeRepository _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        public LikeService(ILikeRepository repository, IMapper mapper, ILogActionRepository logAction, ILogErrorRepository logError) 
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
        }
        public async Task<bool> ToggleLikeAsync(int libroId, int usuarioId)
        {
            try
            {
                return await _repository.ToggleLikeAsync(libroId, usuarioId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ToggleLikeByUserAsync(int libroId, string token)
        {
            try
            {
                return await _repository.ToggleLikeByUserAsync(libroId, token);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<LikesDTO?> GetLibroLikesInfoAsync(int libroId)
        {
            try
            {
                return await _repository.GetLibroLikesInfoAsync(libroId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<LibroLikesDTO>> GetAllLikesInfoAsync()
        {
            try
            {
                return await _repository.GetAllLikesInfoAsync();
            }
            catch (Exception ex)
            {
                return new List<LibroLikesDTO>();
            }
        }

        public async Task<List<ObrasDTO>> GetLikesByUserAsync(string token)
        {
            try
            {
                return await _repository.GetLikesByUserAsync(token);
            }
            catch (Exception ex)
            {
                return new List<ObrasDTO>();
            }
        }

        public async Task<bool> DeleteAllLikesAsync()
        {
            try
            {
                return await _repository.DeleteAllLikesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

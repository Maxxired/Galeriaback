﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.DTO.Likes;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Likes;

namespace Galeria.Application.Interfaces.Likes
{
    public interface ILikeService
    {
        Task<bool> ToggleLikeAsync(int libroId, int usuarioId);

        Task<bool> ToggleLikeByUserAsync(int libroId, string token);

        Task<LikesDTO?> GetLibroLikesInfoAsync(int libroId);

        Task<List<LibroLikesDTO>> GetAllLikesInfoAsync();

        Task<List<ObrasDTO>> GetLikesByUserAsync(string token);

        Task<bool> DeleteAllLikesAsync();
        Task<(List<ObraLikesDTO> Items, int Total)> GetAllLikesFilterAsync(
        int? page = null, int? limit = null,
        string? orderBy = null, string? orderDirection = "asc",
        DateTime? startDate = null, DateTime? endDate = null,
        string? filterField = null, string? filterValue = null,
        string? relationField = null, int? relationId = null);
    }
}

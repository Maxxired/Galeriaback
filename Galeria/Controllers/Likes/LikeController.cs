using Galeria.API.Controllers.BaseGeneric;
using Galeria.Application.Interfaces.Likes;
using Galeria.Application.Interfaces.Usuarios.Artistas;
using Galeria.Application.Services.Likes;
using Galeria.Domain.DTO.Likes;
using Galeria.Domain.Entities.Likes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Galeria.API.Controllers.Likes
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _service;
        public LikeController(ILikeService service)
        {
            _service = service;
        }
        [HttpPost("toggle/{libroId}")]
        [Authorize]
        public async Task<IActionResult> ToggleLike(int libroId, string token)
        {
            try
            {
                bool liked = await _service.ToggleLikeByUserAsync(libroId, token);
                return Ok(new { liked, message = liked ? "Like agregado" : "Like eliminado" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("likes-info/{libroId}")]
        public async Task<IActionResult> GetLibroLikesInfo(int libroId)
        {
            try
            {
                var likeInfo = await _service.GetLibroLikesInfoAsync(libroId);
                return Ok(likeInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("all-likes-info")]
        public async Task<IActionResult> GetAllLikesInfo()
        {
            try
            {
                var allLikesInfo = await _service.GetAllLikesInfoAsync();
                return Ok(allLikesInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("usuario")]
        public async Task<IActionResult> GetLikesByUser(string token)
        {
            var likedBooks = await _service.GetLikesByUserAsync(token);
            return Ok(likedBooks);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-all")]
        public async Task<IActionResult> DeleteAllLikes()
        {
            try
            {
                var result = await _service.DeleteAllLikesAsync();
                if (!result) return NotFound(new { message = "No hay likes para eliminar." });

                return Ok(new { message = "Todos los likes han sido eliminados." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetAllLikes(
        [FromQuery] int? page = null,
        [FromQuery] int? limit = null,
        [FromQuery] string? orderBy = null,
        [FromQuery] string? orderDirection = "asc",
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] string? filterField = null,
        [FromQuery] string? filterValue = null,
        [FromQuery] string? relationField = null,
        [FromQuery] int? relationId = null)
        {
            var result = await _service.GetAllLikesFilterAsync(
                page, limit, orderBy, orderDirection,
                startDate, endDate, filterField, filterValue,
                relationField, relationId);

            return Ok(new
            {
                Total = result.Total,
                Likes = result.Items
            });
        }
    }
}

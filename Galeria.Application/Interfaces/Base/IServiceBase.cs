using Galeria.Domain.DTO;
using System.Linq.Expressions;
using Galeria.Domain.Common.ViewModels.Util;

namespace Galeria.Application.Interfaces.Base
{
    public interface IServiceBase<T, TDto> where T : class where TDto : BaseDTO
    {
        Task<ResponseHelper> GetAllFilterAsync(
        int? page = null, int? limit = null,
        string? orderBy = null, string? orderDirection = "asc",
        DateTime? startDate = null, DateTime? endDate = null,
        string? filterField = null, string? filterValue = null,
        string? relationField = null, int? relationId = null);
        Task<ResponseHelper> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<ResponseHelper> InsertAsync(T entity);
        Task<ResponseHelper> UpdateAsync(T entity);
        Task<ResponseHelper> GetById(Expression<Func<T, bool>>? filter = null);
        Task<ResponseHelper> RemoveAsync(T entity);
        Task<ResponseHelper> RemoveAsync(int Id);
        Task<TDto> ConvertToDto(T entity);
        Task<T> ConvertToEntity(TDto dto);
        Task<List<TDto>> ConvertToDto(List<T> entity);
        Task<List<T>> ConvertToEntity(List<TDto> dto);
    }
}

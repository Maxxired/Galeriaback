using AutoMapper;
using Galeria.Domain.DTO;
using System.Linq.Expressions;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Infraestructure.Repositories.Generic;
using Galeria.Application.Extensions;
using Serilog;
using Galeria.Application.Interfaces.Base;
using Galeria.Infraestructure.Interfaces.Log;
using System.Text.Json;

namespace Galeria.Application.Services.Base
{
    public class ServiceBase<T, TDto> : IServiceBase<T, TDto> where T : class where TDto : BaseDTO
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<T> _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        public ServiceBase(IMapper mapper, IBaseRepository<T> baseRepository, ILogActionRepository logAction, ILogErrorRepository logError)
        {
            _mapper = mapper;
            _repository = baseRepository;
            _LogError = logError;
            _LogAction = logAction;
        }

        public async Task LogAction(string action, string details = null)
        {
            await _LogAction.LogActionAsync($"{action} en la entidad {typeof(T).GetDisplayName()}", details);
        }
        public async Task LogError(Exception ex)
        {
            await _LogError.LogErrorAsync(ex, typeof(T).GetDisplayName());
        }

        public virtual async Task<ResponseHelper> InsertAsync(T entity)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var result = await _repository.InsertAsync(entity);

                if (result != 0)
                {
                    response.Message = $"El elemento {typeof(T).GetDisplayName()} fue insertado con éxito.";
                    response.Success = true;
                    response.Data = entity;

                    string dataAsJson = JsonSerializer.Serialize(response.Data);
                    await LogAction("InsertAsync", dataAsJson);

                    Log.Information(response.Message);

                    var idProperty = entity.GetType().GetProperty("Id");
                    if (idProperty != null && idProperty.CanWrite)
                    {
                        idProperty.SetValue(entity, result, null);
                    }
                    response.Data = entity;

                }
            }
            catch (Exception e)
            {
                await LogError(e);
                Log.Error(e.Message);
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ResponseHelper> UpdateAsync(T entity)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var result = await _repository.UpdateAsync(entity);

                if (result > 0)
                {
                    response.Message = $"El elemento {typeof(T).GetDisplayName()} fue actualizado con éxito.";
                    response.Success = true;
                    response.Data = entity;

                    string dataAsJson = JsonSerializer.Serialize(response.Data);
                    await LogAction("UpdateAsync", dataAsJson);

                    Log.Information(response.Message);
                }
            }
            catch (Exception e)
            {
                await LogError(e);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> RemoveAsync(T entity)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var result = await _repository.RemoveAsync(entity);

                if (result > 0)
                {
                    response.Message = $"El elemento  {typeof(T).GetDisplayName()} fue eliminado con éxito.";

                    response.Success = true;
                    response.Data = entity;

                    string dataAsJson = JsonSerializer.Serialize(response.Data);
                    await LogAction("RemoveAsync", dataAsJson);

                    Log.Information(response.Message);
                }
            }
            catch (Exception e)
            {
                await LogError(e);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> RemoveAsync(int id)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var result = await _repository.RemoveAsync(id);

                if (result > 0)
                {
                    response.Message = $"El elemento  {typeof(T).GetDisplayName()} fue eliminado con éxito.";


                    response.Success = true;
                    response.Data = result;

                    string dataAsJson = JsonSerializer.Serialize(response.Data);
                    await LogAction("RemoveAsync", dataAsJson);

                    Log.Information(response.Message);
                }
            }
            catch (Exception e)
            {
                await LogError(e);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> GetById(Expression<Func<T, bool>>? filter = null)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var data = await _repository.GetSingleAsync(filter);

                response.Success = true;
                response.Data = data;

                string dataAsJson = JsonSerializer.Serialize(response.Data);
                await LogAction("GetById", dataAsJson);

            }
            catch (Exception e)
            {
                await LogError(e);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<ResponseHelper> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var data = await _repository.GetAllAsync(filter);

                response.Success = true;
                response.Data = data;

                string dataAsJson = JsonSerializer.Serialize(response.Data);
                await LogAction("GetAllAsync", dataAsJson);
            }
            catch (Exception e)
            {
                await LogError(e);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<ResponseHelper> GetAllFilterAsync(
            int? page = null, int? limit = null,
            string? orderBy = null, string? orderDirection = "asc",
            DateTime? startDate = null, DateTime? endDate = null,
            string? filterField = null, string? filterValue = null,
            string? relationField = null, int? relationId = null)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var data = await _repository.GetAllFilterAsync(page, limit, orderBy, orderDirection, startDate, endDate, filterField, filterValue, relationField, relationId);

                var items = data.Items;
                var total = data.Total;

                response.Success = true;
                response.Data = new
                {
                    Items = items,
                    Total = total
                };
            }
            catch (Exception e)
            {
                await LogError(e);
                response.Message = e.Message;
            }
            return response;
        }


        public async Task<TDto> ConvertToDto(T entity)
        {
            return await Task.FromResult(_mapper.Map<TDto>(entity));
        }
        public async Task<T> ConvertToEntity(TDto dto)
        {
            return await Task.FromResult(_mapper.Map<T>(dto));
        }
        public async Task<List<TDto>> ConvertToDto(List<T> entities)
        {
            return await Task.FromResult(_mapper.Map<List<TDto>>(entities));
        }
        public async Task<List<T>> ConvertToEntity(List<TDto> dto)
        {
            return await Task.FromResult(_mapper.Map<List<T>>(dto));
        }
    }
}

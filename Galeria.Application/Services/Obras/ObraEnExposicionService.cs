using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Galeria.Application.Interfaces.Obras;
using Galeria.Application.Services.Base;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Obras;
using Galeria.Infraestructure.Interfaces.Log;
using Galeria.Infraestructure.Interfaces.Obras;
using Galeria.Infraestructure.Repositories.Obras;

namespace Galeria.Application.Services.Obras
{
    public class ObraEnExposicionService: ServiceBase<ObraEnExposicion, ObraEnExposicionDTO>, IObraEnExposicionService
    {
        private readonly IMapper _mapper;
        private readonly IObraEnExposicionRepository _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        public ObraEnExposicionService(IObraEnExposicionRepository repository, IMapper mapper, ILogActionRepository logAction, ILogErrorRepository logError) : base(mapper, repository, logAction, logError)
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
        }
        public async Task<bool> CrearRelacionAsync(int idObra, int idExposicion)
        {
            try
            {
                var relacion = new ObraEnExposicion
                {
                    IdObra = idObra,
                    IdExposicion = idExposicion
                };

                await _repository.CrearRelacionAsync(idObra, idExposicion);
                string dataAsJson = JsonSerializer.Serialize(relacion);
                await LogAction("CrearRelacionAsync", dataAsJson);
                return true;
            }
            catch (Exception ex)
            {
                await LogError(ex);
                return false;
            }
        }

        public async Task<bool> EliminarRelacionAsync(int idObra, int idExposicion)
        {
            try
            {
                var relacion = await _repository.GetSingleAsync(o => o.IdObra == idObra && o.IdExposicion == idExposicion);

                if (relacion == null)
                    return false;

                await _repository.CrearRelacionAsync(idObra, idExposicion);
                string dataAsJson = JsonSerializer.Serialize(relacion);
                await LogAction("EliminarRelacionAsync", dataAsJson);
                return true;
            }
            catch (Exception ex)
            {
                await LogError(ex);
                return false;
            }
        }

        public async Task<List<Obra>> ListarObrasDeExposicionAsync(int idExposicion)
        {
            try
            {
                return await _repository.ListarObrasDeExposicionAsync(idExposicion);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                return new List<Obra>();
            }
        }
    }
}

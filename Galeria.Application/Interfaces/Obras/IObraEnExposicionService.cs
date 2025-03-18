using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Obras;

namespace Galeria.Application.Interfaces.Obras
{
    public interface IObraEnExposicionService : IServiceBase<ObraEnExposicion, ObraEnExposicionDTO>
    {
        Task<bool> CrearRelacionAsync(int idObra, int idExposicion);

        Task<bool> EliminarRelacionAsync(int idObra, int idExposicion);

        Task<List<Obra>> ListarObrasDeExposicionAsync(int idExposicion);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Obras;
using Galeria.Infraestructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Galeria.Infraestructure.Interfaces.Obras
{
    public interface IObraEnExposicionRepository : IBaseRepository<ObraEnExposicion>
    {
        Task<bool> CrearRelacionAsync(int idObra, int idExposicion);

        Task<bool> EliminarRelacionAsync(int idObra, int idExposicion);

        Task<List<Obra>> ListarObrasDeExposicionAsync(int idExposicion);
    }
}

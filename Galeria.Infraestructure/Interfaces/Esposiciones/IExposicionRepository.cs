using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Exposiciones;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Interfaces.Esposiciones
{
    public interface IExposicionRepository : IBaseRepository<Exposicion>
    {
    }
}

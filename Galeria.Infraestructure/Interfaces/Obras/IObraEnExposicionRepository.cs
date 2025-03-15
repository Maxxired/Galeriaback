using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Obras;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Interfaces.Obras
{
    public interface IObraEnExposicionRepository : IBaseRepository<ObraEnExposicion>
    {
    }
}

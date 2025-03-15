using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.DTO.Exposiciones;
using Galeria.Domain.Entities.Exposiciones;

namespace Galeria.Application.Interfaces.Exposiciones
{
    public interface IExposicionService: IServiceBase<Exposicion, ExposicionDTO>
    {
    }
}

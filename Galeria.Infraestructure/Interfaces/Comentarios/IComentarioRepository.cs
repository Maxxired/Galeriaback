using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Comentarios;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Interfaces.Comentarios
{
    public interface IComentarioRepository : IBaseRepository<Comentario>
    {
    }
}

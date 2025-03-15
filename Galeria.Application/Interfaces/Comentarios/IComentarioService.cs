using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.DTO.Comentarios;
using Galeria.Domain.Entities.Comentarios;

namespace Galeria.Application.Interfaces.Comentarios
{
    public interface IComentarioService: IServiceBase<Comentario,ComentarioDTO>
    {
    }
}

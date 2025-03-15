using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.DTO.Categorias;
using Galeria.Domain.Entities.Categorias;

namespace Galeria.Application.Interfaces.Categorias
{
    public interface ICategoriaService : IServiceBase<Categoria,CategoriaDTO>
    {
    }
}

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
    public interface IObraCategoriaRepository : IBaseRepository<ObraCategoria>
    {
        Task RemoveByObraIdAsync(int idObra);
        Task InsertManyAsync(List<ObraCategoria> categorias);
    }
}

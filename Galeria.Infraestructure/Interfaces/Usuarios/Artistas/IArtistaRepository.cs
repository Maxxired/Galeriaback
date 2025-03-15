using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Usuarios.Artistas;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Interfaces.Usuarios.Artistas
{
    public interface IArtistaRepository : IBaseRepository<Artista>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.DTO.Usuarios.Artistas;
using Galeria.Domain.Entities.Usuarios.Artistas;

namespace Galeria.Application.Interfaces.Usuarios.Artistas
{
    public interface IArtistaService : IServiceBase<Artista, ArtistaDTO>
    {
    }
}

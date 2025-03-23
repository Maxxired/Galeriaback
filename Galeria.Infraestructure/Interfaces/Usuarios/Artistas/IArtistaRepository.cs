using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Common.ViewModels.Artistas;
using Galeria.Domain.Common.ViewModels.Personas;
using Galeria.Domain.Entities.Usuarios.Artistas;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Interfaces.Usuarios.Artistas
{
    public interface IArtistaRepository : IBaseRepository<Artista>
    {
        Task<int> ActualizarPerfilArtista(string idApplicationUser, ActualizarPerfilArtistaVM datos);
        Task<int> SubirFotoPerfilArtista(string idApplicationUser, string url);
        Task<int> ActualizarFotoPerfilArtista(string idApplicationUser, string url);
        Task<int> EliminarFotoPerfilArtista(string idApplicationUser);
        Task<DatosPerfilArtistaVM> GetDatosPerfil(string idApplicationUser);
    }
}

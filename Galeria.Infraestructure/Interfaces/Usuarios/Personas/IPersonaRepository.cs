using Galeria.Domain.Common.ViewModels.Personas;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Infraestructure.Repositories.Generic;

namespace Galeria.Infraestructure.Interfaces.Usuarios.Personas
{
    public interface IPersonaRepository : IBaseRepository<Persona>
    {
        Task<int> ActualizarPerfilUsuario(string idApplicationUser, ActualizarPerfilVM datos);
        Task<int> SubirFotoPerfil(string idApplicationUser, string url);
        Task<int> ActualizarFotoPerfil(string idApplicationUser, string url);
        Task<int> EliminarFotoPerfil(string idApplicationUser);
        Task<DatosPerfilVM> GetDatosPerfil(string idApplicationUser);
        Task<List<DatosPerfilVM>> GetTodosLosUsuarios();
    }
}
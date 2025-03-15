using Microsoft.AspNetCore.Http;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.Common.ViewModels.Personas;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Domain.DTO.Usuarios.Personas;

namespace Galeria.Application.Interfaces.Usuarios.Personas
{
    public interface IPersonaService: IServiceBase<Persona, PersonaDTO>
    {
        Task<ResponseHelper> ActualizarPerfilUsuario(string idApplicationUser, ActualizarPerfilVM datos);
        Task<ResponseHelper> SubirFotoPerfil(string idApplicationUser, IFormFile archivo);
        Task<ResponseHelper> ActualizarFotoPerfil(string idApplicationUser, IFormFile archivo);
        Task<ResponseHelper> EliminarFotoPerfil(string idApplicationUser);
        Task<ResponseHelper> GetDatosPerfil(string idApplicationUser);
        Task<ResponseHelper> GetTodosLosUsuarios();
    }
}

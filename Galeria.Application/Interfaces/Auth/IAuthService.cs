using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Auth;
using Galeria.Domain.DTO.Usuarios.Artistas;

namespace Galeria.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<ResponseHelper> CreateAccount(UserDTO userDTO);
        Task<ResponseHelperAuth> LoginAccount(LoginDTO loginDTO);
        Task<ResponseHelperAuth> RegistrarUsuario(RegistrarUsuarioDTO socio);
        Task<ResponseHelperAuth> RegistrarArtista(RegistrarArtistaDTO socio);
        Task<ResponseHelperAuth> RefreshTokenAsync(string token);
    }
}

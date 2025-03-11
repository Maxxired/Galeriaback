using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Auth;

namespace Galeria.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<ResponseHelper> CreateAccount(UserDTO userDTO);
        Task<ResponseHelperAuth> LoginAccount(LoginDTO loginDTO);
        Task<ResponseHelperAuth> RegistrarUsuario(RegistrarUsuarioDTO socio);
        Task<ResponseHelperAuth> RefreshTokenAsync(string token);
    }
}

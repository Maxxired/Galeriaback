using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Auth;
using Galeria.Domain.DTO.Usuarios.Artistas;
using Galeria.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Galeria.Infraestructure.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<ResponseHelper> CreateAccount(UserDTO userDTO);
        Task<ResponseHelperAuth> LoginAccount(LoginDTO loginDTO);
        Task<ResponseHelperAuth> CrearCuentaUsuario(RegistrarUsuarioDTO socio);
        Task<ResponseHelperAuth> CrearCuentaArtista(RegistrarArtistaDTO socio);
        Task<string> GenerateToken(UserSession user);
        string GenerateRefreshToken();
        Task SaveRefreshToken(ApplicationUser user, string refreshToken);
        Task<ApplicationUser?> GetUserByRefreshToken(string refreshToken);
        Task<ResponseHelperAuth> RefreshToken(string refreshToken);
    }
}

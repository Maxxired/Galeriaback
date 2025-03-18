using Microsoft.AspNetCore.Mvc;
using Galeria.Application.Interfaces.Auth;
using Galeria.Domain.DTO.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Galeria.Domain.DTO.Usuarios.Artistas;

namespace Galeria.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        [Authorize]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var response = await authService.CreateAccount(userDTO);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await authService.LoginAccount(loginDTO);
            return Ok(response);
        }

        [HttpPost("registrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario(RegistrarUsuarioDTO registrarUsuario)
        {
            var response = await authService.RegistrarUsuario(registrarUsuario);
            return Ok(response);
        }
        [HttpPost("registrarArtista")]
        public async Task<IActionResult> RegistrarArtista(RegistrarArtistaDTO registrarUsuario)
        {
            var response = await authService.RegistrarArtista(registrarUsuario);
            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string token)
        {
            var response = await authService.RefreshTokenAsync(token);

            return Ok(new { AccessToken = response.Token, RefreshToken = response.RefreshToken });
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Galeria.Application.Interfaces.Auth;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Auth;
using Galeria.Domain.Entities;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Infraestructure.Interfaces.Usuarios.Personas;
using Galeria.Infraestructure.Interfaces.Auth;
using Microsoft.EntityFrameworkCore;
using Galeria.Infraestructure;
using Serilog;
using Galeria.Infraestructure.Interfaces.Log;
using System.Text.Json;
using Azure;

namespace Galeria.Application.Services.Auth
{
    public class AuthService:  IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthRepository _authRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;
        private readonly ApplicationDbContext _context;

        public AuthService( IAuthRepository authRepository, IPersonaRepository personaRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogActionRepository logAction, ILogErrorRepository logError)
        {
            _authRepository = authRepository;
            _personaRepository = personaRepository;
            _userManager = userManager;
            _context = context;
            _LogAction = logAction;
            _LogError = logError;
        }
        public async Task LogAction(string action, string details = null)
        {
            await _LogAction.LogActionAsync($"{action} en la entidad Auth", details);
        }
        public async Task LogError(Exception ex)
        {
            await _LogError.LogErrorAsync(ex, "Auth");
        }

        public async Task<ResponseHelper> CreateAccount(UserDTO userDTO)
        {
            ResponseHelper response = new();
            try
            {
                response = await _authRepository.CreateAccount(userDTO);
                string dataAsJson = JsonSerializer.Serialize(response.Data);
                await LogAction("CreateAccount", dataAsJson);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                await LogError(e);
            }
            return response;
        }

        public async Task<ResponseHelperAuth> LoginAccount(LoginDTO loginDTO)
        {
            ResponseHelperAuth response = new();
            try
            {
                var result = await _authRepository.LoginAccount(loginDTO);
                if (result.Success)
                {
                    response = result;
                    var nombrePersona = await _personaRepository.GetSingleAsync(p => p.IdApplicationUser == response.User.Id);
                    response.User.Nombre = $"{nombrePersona.Nombres} {nombrePersona.Apellidos}".Trim();
                } else
                {
                    response.Message = result.Message;
                }
                string dataAsJson = JsonSerializer.Serialize(response);
                await LogAction("LoginAccount", dataAsJson);

            }
            catch (Exception e)
            {
                response.Message = e.Message;
                await LogError(e);
            }
            return response;
        }

        public async Task<ResponseHelperAuth> RegistrarUsuario(RegistrarUsuarioDTO socio)
        {
            ResponseHelperAuth response = new();

            try
            {
                if (socio == null)
                {
                    response.Success = false;
                    response.Message = "Datos inválidos";
                    return response;
                }

                response = await _authRepository.CrearCuentaUsuario(socio);

                if (!response.Success)
                {
                    return response;
                }

                string dataAsJson = JsonSerializer.Serialize(response);
                await LogAction("RegistrarUsuario", dataAsJson);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                response.Success = false;
                response.Message = "Error interno del servidor";
                await LogError(e);
            }

            return response;
        }

        public async Task<ResponseHelperAuth> RefreshTokenAsync(string token)
        {
            ResponseHelperAuth response = new();
            try
            {
                if (token == null)
                {
                    response.Success = false;
                    response.Message = "Datos inválidos";
                    return response;
                }

                response = await _authRepository.RefreshToken(token);

                if (!response.Success)
                {
                    return response;
                }

                string dataAsJson = JsonSerializer.Serialize(response);
                await LogAction("RefreshTokenAsync", dataAsJson);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                response.Success = false;
                response.Message = "Error interno del servidor";
                await LogError(e);
            }
            return response;
        }
    }
}

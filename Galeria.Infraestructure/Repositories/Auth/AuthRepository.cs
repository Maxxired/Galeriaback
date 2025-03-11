using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Auth;
using Galeria.Domain.Entities;
using Galeria.Infraestructure.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Infraestructure.Interfaces.Usuarios.Personas;

namespace Galeria.Infraestructure.Repositories.Auth
{
    public class AuthRepository(

        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IPersonaRepository persona,
        IConfiguration config)
        : IAuthRepository
    {
        public async Task<ResponseHelper> CreateAccount(UserDTO userDTO)
        {
            if (userDTO is null) return new ResponseHelper() { Success = false, Message = "sin datos" };
            var newUser = new ApplicationUser()
            {
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new ResponseHelper() { Success = false, Message = "Usuario ya se encuentra registrado" }; ;

            var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded) return new ResponseHelper() { Success = false, Message = "Ha ocurrido un error" };

            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                return new ResponseHelper() { Success = true, Message = "Cuenta creada con éxito." };
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" }); 

                return new ResponseHelper() { Success = true, Message = "Cuenta creada" };
            }
        }

        public async Task<ResponseHelperAuth> CrearCuentaUsuario(RegistrarUsuarioDTO socio)
        {
            ResponseHelperAuth response = new();

            if (socio is null) return new ResponseHelperAuth() { Success = false, Message = "Datos inválidos" };

            var userExistente = await userManager.FindByEmailAsync(socio.Email);
            if (userExistente is not null) return new ResponseHelperAuth() { Success = false, Message = "El usuario ya está registrado" };

            var nuevoUsuario = new ApplicationUser()
            {
                Email = socio.Email,
                UserName = socio.Email
            };

            var crearUsuario = await userManager.CreateAsync(nuevoUsuario, socio.Password);
            if (!crearUsuario.Succeeded) return new ResponseHelperAuth() { Success = false, Message = "Error al crear usuario" };

            var rolUsuario = await roleManager.FindByNameAsync("User");
            if (rolUsuario is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            }

            await userManager.AddToRoleAsync(nuevoUsuario, "User");

            Persona nuevaPersona = new()
            {
                IdApplicationUser = nuevoUsuario.Id,
                Nombres = socio.Nombres,
                Apellidos = socio.Apellidos
            };

            var registroPersona = await persona.InsertAsync(nuevaPersona);
            if (registroPersona == 0) return new ResponseHelperAuth() { Success = false, Message = "Error al registrar datos personales" };

            var roles = await userManager.GetRolesAsync(nuevoUsuario);
            string userRole = roles.FirstOrDefault() ?? "User";

            var userSession = new UserSession(nuevoUsuario.Id, nuevoUsuario.Email, userRole);
            string token = GenerateToken(userSession);
            string refreshToken = GenerateRefreshToken();

            await SaveRefreshToken(nuevoUsuario, refreshToken);

            response.Success = true;
            response.Message = "Usuario registrado correctamente";
            response.Token = token;
            response.RefreshToken = refreshToken;

            return response;
        }


        public async Task<ResponseHelperAuth> LoginAccount(LoginDTO loginDTO)
        {

            ResponseHelperAuth response = new();

            if (loginDTO == null)
            {
                response.Success = false;
                response.Message = "NO se encuentran los datos";
                return response;
            }

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new ResponseHelperAuth() { Success = false, Message = "Usuario no encontrado" };

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new ResponseHelperAuth() { Success = false, Message = "Usuario o contraseña incorrectos" };

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);

            var refreshToken = GenerateRefreshToken();
            await SaveRefreshToken(getUser, refreshToken);

            response.Success = true;
            response.Message = "Acceso correcto";
            response.Token = token;
            response.RefreshToken = refreshToken;
            response.User.Id = getUser.Id;
            response.User.Rol = getUserRole.First();
            response.User.Email = getUser.Email;
            response.User.AvatarURL = getUser.AvatarURL;

            return response;
        }

        public string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task SaveRefreshToken(ApplicationUser user, string refreshToken)
        {
            var existingToken = await userManager.GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken");

            if (existingToken != null)
            {
                await userManager.RemoveAuthenticationTokenAsync(user, "MyApp", "RefreshToken");
            }

            await userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", refreshToken);
        }

        public async Task<ApplicationUser?> GetUserByRefreshToken(string refreshToken)
        {
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var storedToken = await userManager.GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken");
                if (storedToken == refreshToken)
                {
                    return user;
                }
            }
            return null;
        }
        public async Task<ResponseHelperAuth> RefreshToken(string refreshToken)
        {
            ResponseHelperAuth response = new();

            var user = await GetUserByRefreshToken(refreshToken);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Refresh Token inválido";
                return response;
            }

            var roles = await userManager.GetRolesAsync(user);
            var userSession = new UserSession(user.Id, user.Email, roles.First());
            var newAccessToken = GenerateToken(userSession);
            var newRefreshToken = GenerateRefreshToken();

            await SaveRefreshToken(user, newRefreshToken);

            response.Success = true;
            response.Message = "Token actualizado";
            response.Token = newAccessToken;
            response.RefreshToken = newRefreshToken;

            return response;
        }
    }
}

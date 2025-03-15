using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Galeria.Application.Interfaces.Usuarios.Personas;
using Galeria.Application.Services.Base;
using Galeria.Domain.Common.ViewModels.Personas;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Infraestructure.Interfaces.Usuarios.Personas;
using Serilog;
using Galeria.Infraestructure.Interfaces.Log;
using System.Text.Json;
using Galeria.Domain.DTO.Usuarios.Personas;

namespace Galeria.Application.Services.Usuarios.Personas
{
    public class PersonaService : ServiceBase<Persona, PersonaDTO>, IPersonaService
    {

        private readonly IMapper _mapper;
        private readonly IPersonaRepository _repository;
        private readonly ILogActionRepository _LogAction;
        private readonly ILogErrorRepository _LogError;

        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];

        public PersonaService(IPersonaRepository repository, IMapper mapper, IWebHostEnvironment env, ILogActionRepository logAction, ILogErrorRepository logError) : base(mapper, repository, logAction, logError)
        {
            _mapper = mapper;
            _repository = repository;
            _LogAction = logAction;
            _LogError = logError;
            _env = env;
        }
        public async Task<ResponseHelper> ActualizarPerfilUsuario(string idApplicationUser, ActualizarPerfilVM datos)
        {
            ResponseHelper response = new();

            try
            {
                var obtenerUsuario = await _repository.GetAllAsync(x => x.IdApplicationUser == idApplicationUser);

                if (obtenerUsuario == null)
                {
                    response.Message = $"No se encontró el usuario con el id {idApplicationUser}";
                    return response;
                }

                var result = await _repository.ActualizarPerfilUsuario(idApplicationUser, datos);

                if (result != 0)
                {
                    response.Success = true;
                    response.Message = "El perfil del usuario fue actualizado con éxito.";
                    response.Data = result;

                    string dataAsJson = JsonSerializer.Serialize(response.Data);
                    await LogAction("ActualizarPerfilUsuario", dataAsJson);

                    Log.Information(response.Message);
                }
                else
                {
                    response.Message = "No se pudo actualizar el perfil del usuario.";
                }
            }
            catch (Exception e)
            {
                await LogError(e);
                Log.Error(e.Message);
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> SubirFotoPerfil(string idApplicationUser, IFormFile archivo)
        {
            var _uploadBasePath = GetUploadBasePath();

            if (archivo == null || archivo.Length == 0)
            {
                throw new ArgumentException("El archivo no puede ser nulo o estar vacío.", nameof(archivo));
            }

            var extension = Path.GetExtension(archivo.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("El tipo de archivo no es permitido. Solo se aceptan .jpg, .jpeg, .png y .gif.");
            }

            ResponseHelper response = new();

            try
            {
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(_uploadBasePath, fileName);

                CrearCarpetaSiNoExiste(_uploadBasePath);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                var rutaArchivo = Path.Combine("Uploads", "FotosPerfiles", fileName);

                var query = await _repository.SubirFotoPerfil(idApplicationUser, rutaArchivo);

                response.Success = query > 0;
                response.Message = query > 0 ? "La imagen fue subida con éxito." : "No se pudo subir la imagen.";

                string dataAsJson = JsonSerializer.Serialize(new { idApplicationUser, rutaArchivo });
                await LogAction("SubirFotoPerfil", dataAsJson);

                Log.Information(response.Message);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                Log.Error(ex.Message);
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> ActualizarFotoPerfil(string idApplicationUser, IFormFile archivo)
        {
            var _uploadBasePath = GetUploadBasePath();

            if (archivo == null || archivo.Length == 0)
            {
                throw new ArgumentException("El archivo no puede ser nulo o estar vacío.", nameof(archivo));
            }

            var extension = Path.GetExtension(archivo.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("El tipo de archivo no es permitido. Solo se aceptan .jpg, .jpeg, .png y .gif.");
            }

            ResponseHelper response = new();

            try
            {
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(_uploadBasePath, fileName);

                CrearCarpetaSiNoExiste(_uploadBasePath);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                var rutaArchivo = Path.Combine("Uploads", "FotosPerfiles", fileName);

                var query = await _repository.ActualizarFotoPerfil(idApplicationUser, rutaArchivo);

                response.Success = query > 0;
                response.Message = query > 0 ? "La imagen fue actualizada con éxito." : "No se pudo actualizar la imagen.";

                string dataAsJson = JsonSerializer.Serialize(new { idApplicationUser, rutaArchivo });
                await LogAction("ActualizarFotoPerfil", dataAsJson);

                Log.Information(response.Message);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                Log.Error(ex.Message);
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> EliminarFotoPerfil(string idApplicationUser)
        {
            ResponseHelper response = new();

            try
            {
                var query = await _repository.EliminarFotoPerfil(idApplicationUser);

                response.Success = query > 0;
                response.Message = query > 0 ? "La imagen fue eliminada con éxito." : "No se pudo eliminar la imagen.";

                await LogAction("EliminarFotoPerfil", idApplicationUser);

                Log.Information(response.Message);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                Log.Error(ex.Message);
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> GetDatosPerfil(string idApplicationUser)
        {
            ResponseHelper response = new();

            try
            {
                var query = await _repository.GetDatosPerfil(idApplicationUser);

                response.Success = query != null;
                response.Message = query != null ? "Datos del perfil obtenidos con éxito." : "No se pudieron obtener los datos del perfil.";
                response.Data = query;

                string dataAsJson = JsonSerializer.Serialize(response.Data);
                await LogAction("GetDatosPerfil", dataAsJson);

                Log.Information(response.Message);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                Log.Error(ex.Message);
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseHelper> GetTodosLosUsuarios()
        {
            ResponseHelper response = new();

            try
            {
                var query = await _repository.GetTodosLosUsuarios();

                response.Success = query != null;
                response.Message = query != null ? "Datos de los usuarios obtenidos con éxito." : "No se pudieron obtener los datos de los usuarios.";
                response.Data = query;

                string dataAsJson = JsonSerializer.Serialize(response.Data);
                await LogAction("GetTodosLosUsuarios", dataAsJson);

                Log.Information(response.Message);
            }
            catch (Exception ex)
            {
                await LogError(ex);
                Log.Error(ex.Message);
                response.Message = ex.Message;
            }

            return response;
        }

        private string GetUploadBasePath()
        {
            return Path.Combine(_env.WebRootPath, "Uploads", "FotosPerfiles");
        }

        private void CrearCarpetaSiNoExiste(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }



    }
}

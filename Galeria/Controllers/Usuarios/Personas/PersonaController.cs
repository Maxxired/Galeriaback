using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Galeria.Application.Interfaces.Usuarios.Personas;
using Galeria.Domain.Common.ViewModels.Personas;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Usuarios.Personas;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.API.Controllers.BaseGeneric;

namespace Galeria.WebAPI.Controllers.Usuarios.Personas
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : BaseController<Persona, PersonaDTO>
    {
        private readonly IPersonaService _service;
        public PersonaController(IPersonaService service)
             : base(service)
        {
            _service = service;
        }

        [HttpPut("ActualizarPerfilUsuario/{idApplicationUser}")]
        public async Task<IActionResult> ActualizarPerfilUsuario(string idApplicationUser, ActualizarPerfilVM datos)
        {
            var result = await _service.ActualizarPerfilUsuario(idApplicationUser, datos);
            return Ok(result);
        }

        [HttpPost("SubirFotoPerfil")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SubirFotoPerfil([FromForm] FormImagenPerfilVM request)
        {
            if (request.IdApplicationUser.Length < 0)
            {
                return BadRequest(new { message = "El ID de la persona es inválido." });
            }

            if (request.Archivo == null || request.Archivo.Length == 0)
            {
                return BadRequest(new { message = "Debe proporcionar un archivo válido para subir." });
            }

            try
            {
                var response = await _service.SubirFotoPerfil(request.IdApplicationUser, request.Archivo);

                if (response.Success)
                {
                    return Ok(response);
                }

                return StatusCode(500, new { message = response.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", error = ex.Message });
            }
        }

        [HttpDelete("EliminarFotoPerfil/{idApplicationUser}")]
        public async Task<IActionResult> EliminarFotoPerfil(string idApplicationUser)
        {
            var result = await _service.EliminarFotoPerfil(idApplicationUser);
            return Ok(result);
        }

        [HttpPut("ActualizarFotoPerfil")]
        public async Task<IActionResult> ActualizarFotoPerfil([FromForm] FormImagenPerfilVM request)
        {
            if (request.IdApplicationUser.Length < 0)
            {
                return BadRequest(new { message = "El ID de la persona es inválido." });
            }

            if (request.Archivo == null || request.Archivo.Length == 0)
            {
                return BadRequest(new { message = "Debe proporcionar un archivo válido para subir." });
            }

            try
            {
                var response = await _service.ActualizarFotoPerfil(request.IdApplicationUser, request.Archivo);

                if (response.Success)
                {
                    return Ok(response);
                }

                return StatusCode(500, new { message = response.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", error = ex.Message });
            }
        }

        [HttpGet("ObtenerDatosPerfil/{idApplicationUser}")]
        public async Task<IActionResult> ObtenerDatosPerfil(string idApplicationUser)
        {
            var result = await _service.GetDatosPerfil(idApplicationUser);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetTodosLosUsuarios")]
        public async Task<IActionResult> GetTodosLosUsuarios()
        {
            var result = await _service.GetTodosLosUsuarios();
            return Ok(result);
        }

    }
}

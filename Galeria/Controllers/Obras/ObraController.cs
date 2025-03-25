using Galeria.API.Controllers.BaseGeneric;
using Galeria.Application.Interfaces.Likes;
using Galeria.Application.Interfaces.Obras;
using Galeria.Application.Services.Obras;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Obras;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Galeria.Application.Services.Obras.ObraService;

namespace Galeria.API.Controllers.Obras
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraController : BaseController<Obra,ObraDTO>
    {
        private readonly IObraService _service;
        private readonly IObraEnExposicionService _serviceExposicion;
        public ObraController(IObraService service, IObraEnExposicionService serviceExposicion) : base(service)
        {
            _service = service;
            _serviceExposicion = serviceExposicion;
        }
        [HttpPost("exposicion/crear")]
        public async Task<IActionResult> CrearRelacion([FromQuery] int idObra, [FromQuery] int idExposicion)
        {
            var resultado = await _serviceExposicion.CrearRelacionAsync(idObra, idExposicion);
            if (!resultado)
                return BadRequest("No se pudo crear la relación.");

            return Ok("Relación creada correctamente.");
        }

        [HttpDelete("exposicion/eliminar")]
        public async Task<IActionResult> EliminarRelacion([FromQuery] int idObra, [FromQuery] int idExposicion)
        {
            var resultado = await _serviceExposicion.EliminarRelacionAsync(idObra, idExposicion);
            if (!resultado)
                return NotFound("Relación no encontrada o no se pudo eliminar.");

            return Ok("Relación eliminada correctamente.");
        }

        [HttpGet("exposicion/listar/{idExposicion}")]
        public async Task<IActionResult> ListarObrasDeExposicion(int idExposicion)
        {
            var obras = await _serviceExposicion.ListarObrasDeExposicionAsync(idExposicion);
            if (obras == null || obras.Count == 0)
                return NotFound("No hay obras en esta exposición.");

            return Ok(obras);
        }
        [HttpPost("SubirImagenObra/{idObra}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SubirImagenObra([FromForm] FormImagenObraVM request)
        {
            var response = await _service.SubirImagenObra(request.idObra, request.archivo);
            return Ok(response);
        }

        [HttpPut("ActualizarImagenObra/{idObra}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarImagenObra([FromForm] FormImagenObraVM request)
        {
            var response = await _service.ActualizarImagenObra(request.idObra, request.archivo);
            return Ok(response);
        }

        [HttpDelete("EliminarImagenObra/{idObra}")]
        public async Task<IActionResult> EliminarImagenObra(int idObra)
        {
            var response = await _service.EliminarImagenObra(idObra);
            return Ok(response);
        }
        [HttpPost("CrearObra")]
        public async Task<IActionResult> CreateAsync([FromBody] ObraCreateDTO obraDto)
        {
            if (obraDto == null)
                return BadRequest("Datos incompletos.");

            try
            {
                var obra = await _service.CreateObraAsync(obraDto);
                return Ok(obra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("EditarObra/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ObraUpdateDTO obraDto)
        {
            if (obraDto == null)
                return BadRequest("Datos incompletos.");

            var obra = await _service.UpdateObraAsync(id, obraDto);
            if (obra == null)
                return NotFound("Obra no encontrada.");

            return Ok(obra);
        }
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ObraQueryDTO>>> Buscar(
        [FromQuery] string? titulo,
        [FromQuery] string? autor,
        [FromQuery] string? etiqueta,
        [FromQuery] bool populares = false,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20)
        {
            IEnumerable<ObraQueryDTO> libros = new List<ObraQueryDTO>();

            if (!string.IsNullOrEmpty(titulo))
            {
                libros = await _service.GetByTituloAsync(titulo, skip, take);
            }
            else if (!string.IsNullOrEmpty(autor))
            {
                libros = await _service.GetByAutorNombreAsync(autor, skip, take);
            }
            else if (!string.IsNullOrEmpty(etiqueta))
            {
                libros = await _service.GetByEtiquetaNombreAsync(etiqueta, skip, take);
            }
            else if (populares)
            {
                libros = await _service.GetLibrosMasGustadosAsync();
            }
            else
            {
                return BadRequest("Debes especificar un criterio de búsqueda.");
            }

            return Ok(libros);
        }
    }
}

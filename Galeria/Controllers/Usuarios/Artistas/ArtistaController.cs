using Galeria.API.Controllers.BaseGeneric;
using Galeria.Application.Interfaces.Usuarios.Artistas;
using Galeria.Application.Interfaces.Usuarios.Personas;
using Galeria.Domain.DTO.Usuarios.Artistas;
using Galeria.Domain.Entities.Usuarios.Artistas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Galeria.API.Controllers.Usuarios.Artistas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistaController : BaseController<Artista,ArtistaDTO>
    {
        private readonly IArtistaService _service;
        public ArtistaController(IArtistaService service): base(service)
        {
            _service = service;
        }
    }
}

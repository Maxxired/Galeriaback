using Galeria.API.Controllers.BaseGeneric;
using Galeria.Application.Interfaces.Categorias;
using Galeria.Application.Interfaces.Obras;
using Galeria.Domain.DTO.Categorias;
using Galeria.Domain.Entities.Categorias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Galeria.API.Controllers.Categorias
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : BaseController<Categoria,CategoriaDTO>
    {
        private readonly ICategoriaService _service;
        public CategoriaController(ICategoriaService service) : base(service)
        {
            _service = service;
        }
    }
}

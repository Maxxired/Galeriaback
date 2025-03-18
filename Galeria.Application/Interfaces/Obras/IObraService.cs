using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Obras;
using Microsoft.AspNetCore.Http;
using Serilog;
using static Galeria.Application.Services.Obras.ObraService;

namespace Galeria.Application.Interfaces.Obras
{
    public interface IObraService : IServiceBase<Obra, ObraDTO>
    {
        Task<ResponseHelper> SubirImagenObra(int idObra, IFormFile archivo);

        Task<ResponseHelper> ActualizarImagenObra(int idObra, IFormFile archivo);

        Task<ResponseHelper> EliminarImagenObra(int idObra);
        Task<int> CreateObraAsync(ObraCreateDTO obraDto);

        Task<int> UpdateObraAsync(int id, ObraUpdateDTO obraDto);
        Task<IEnumerable<ObraQueryDTO>> GetByTituloAsync(string titulo, int skip, int take);
        Task<IEnumerable<ObraQueryDTO>> GetByEtiquetaNombreAsync(string etiqueta, int skip, int take);

        Task<IEnumerable<ObraQueryDTO>> GetByAutorNombreAsync(string autor, int skip, int take);

        Task<IEnumerable<ObraQueryDTO>> GetLibrosMasGustadosAsync();

        Task<ObrasDTO> GetBySlugAsync(string slug);

        Task<IEnumerable<ObrasDTO>> GetLibrosByAutorAsync(string token);

    }
}

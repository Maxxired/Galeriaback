using System.IdentityModel.Tokens.Jwt;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Obras;
using Galeria.Infraestructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Galeria.Infraestructure.Interfaces.Obras
{
    public interface IObraRepository : IBaseRepository<Obra>
    {
        Task<int> SubirImagenObra(int idObra, string url);

        Task<int> ActualizarImagenObra(int idObra, string url);

        Task<int> EliminarImagenObra(int idObra);
        Task<bool> IsSlugTakenAsync(string slug);
        Task<Obra> GetByIdAsync(int id);
        Task<IEnumerable<ObraQueryDTO>> GetByTituloAsync(string titulo, int skip, int take);
        Task<IEnumerable<ObraQueryDTO>> GetByEtiquetaNombreAsync(string etiqueta, int skip, int take);
        Task<IEnumerable<ObraQueryDTO>> GetByAutorNombreAsync(string autor, int skip, int take);
        Task<IEnumerable<ObraQueryDTO>> GetLibrosMasGustadosAsync();
        Task<ObrasDTO> GetBySlugAsync(string slug);
        Task<IEnumerable<ObrasDTO>> GetLibrosByAutorAsync(string token);
    }
}

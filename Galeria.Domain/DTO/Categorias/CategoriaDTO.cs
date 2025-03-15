using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Obras;

namespace Galeria.Domain.DTO.Categorias
{
    public class CategoriaDTO : BaseDTO
    {
        public string Nombre { get; set; }
        public string DescripcionCategoria { get; set; }
        public string NombreCorto { get; set; }

    }
}

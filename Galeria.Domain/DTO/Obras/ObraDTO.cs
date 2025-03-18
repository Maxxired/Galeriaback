using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Usuarios.Artistas;

namespace Galeria.Domain.DTO.Obras
{
    public class ObraDTO : BaseDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int IdArtista { get; set; }
        public string Slug { get; set; }
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; }
    }
}

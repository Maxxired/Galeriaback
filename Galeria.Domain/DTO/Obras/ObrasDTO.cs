using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeria.Domain.DTO.Obras
{
    public class ObrasDTO : BaseDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ArtistaNombre { get; set; }
        public string Slug { get; set; }
        public int IdArtista { get; set; }
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; }
        public List<int> Categorias { get; set; }
    }
}

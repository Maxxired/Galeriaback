using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeria.Domain.DTO.Likes
{
    public class ObraLikesDTO
    {
        public int LibroId { get; set; }
        public int TotalLikes { get; set; }
        public string TituloLibro { get; set; }
        public string UsuariosQueDieronLike { get; set; }
    }
}

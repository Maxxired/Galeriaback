using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeria.Domain.DTO.Comentarios
{
    public class ComentarioDatosDTO
    {
        public string PersonaNombre { get; set; }
        public string Texto { get; set; }
        public DateTime FechaComentario { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Obras;
using Galeria.Domain.Entities.Usuarios.Personas;

namespace Galeria.Domain.DTO.Comentarios
{
    public class ComentarioDTO : BaseDTO
    {
        public int IdPersona { get; set; }
        public int IdObra { get; set; }
        public string Texto { get; set; }
        public DateTime FechaComentario { get; set; }
    }
}

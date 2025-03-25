using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Obras;
using Galeria.Domain.Entities.Usuarios.Artistas;

namespace Galeria.Domain.DTO.Exposiciones
{
    public class ExposicionDTO : BaseDTO
    {
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdArtista { get; set; }
    }
}

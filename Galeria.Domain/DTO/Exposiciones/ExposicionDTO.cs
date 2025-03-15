using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Obras;

namespace Galeria.Domain.DTO.Exposiciones
{
    public class ExposicionDTO : BaseDTO
    {
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}

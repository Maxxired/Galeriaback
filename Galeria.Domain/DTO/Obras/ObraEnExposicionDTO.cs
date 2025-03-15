using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Exposiciones;

namespace Galeria.Domain.DTO.Obras
{
    public class ObraEnExposicionDTO : BaseDTO
    {
        public int IdObra { get; set; }
        public int IdExposicion { get; set; }
    }
}

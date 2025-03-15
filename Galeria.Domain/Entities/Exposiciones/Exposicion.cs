using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Obras;

namespace Galeria.Domain.Entities.Exposiciones
{
    [Table("Tbl_Exposiciones")]
    public class Exposicion : BaseEntity
    {
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public virtual ICollection<ObraEnExposicion> Obras { get; set; }
    }
}

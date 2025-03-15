using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Exposiciones;

namespace Galeria.Domain.Entities.Obras
{
    [Table("Tbl_ObrasEnExposicion")]
    public class ObraEnExposicion
    {
        [ForeignKey("Obra")]
        public int IdObra { get; set; }
        public virtual Obra Obra { get; set; }

        [ForeignKey("Exposicion")]
        public int IdExposicion { get; set; }
        public virtual Exposicion Exposicion { get; set; }
    }
}

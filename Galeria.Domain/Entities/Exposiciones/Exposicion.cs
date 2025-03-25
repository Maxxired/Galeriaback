using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Obras;
using Galeria.Domain.Entities.Usuarios.Artistas;

namespace Galeria.Domain.Entities.Exposiciones
{
    [Table("Tbl_Exposiciones")]
    public class Exposicion : BaseEntity
    {
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        [ForeignKey("Artista")]
        public int IdArtista { get; set; }
        public virtual Artista Artista { get; set; }
        public virtual ICollection<ObraEnExposicion> Obras { get; set; }
    }
}

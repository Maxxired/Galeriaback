using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Comentarios;
using Galeria.Domain.Entities.Likes;
using static Galeria.Domain.Common.Util.Enums;

namespace Galeria.Domain.Entities.Usuarios.Personas
{
    [Table("Tbl_Personas")]
    public class Persona: BaseEntity
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }

        [ForeignKey("ApplicationUser")]
        public string IdApplicationUser { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Obras;
using Galeria.Domain.Entities.Usuarios.Personas;

namespace Galeria.Domain.Entities.Comentarios
{
    [Table("Tbl_Comentarios")]
    public class Comentario : BaseEntity
    {
        [ForeignKey("Persona")]
        public int IdPersona { get; set; }
        public virtual Persona Persona { get; set; }

        [ForeignKey("Obra")]
        public int IdObra { get; set; }
        public virtual Obra Obra { get; set; }

        public string Texto { get; set; }
        public DateTime FechaComentario { get; set; } = DateTime.Now;
    }
}

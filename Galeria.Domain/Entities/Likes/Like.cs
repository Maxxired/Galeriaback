using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.Entities.Obras;
using Galeria.Domain.Entities.Usuarios.Personas;

namespace Galeria.Domain.Entities.Likes
{
    [Table("Tbl_Likes")]
    public class Like
    {
        [ForeignKey("Persona")]
        public int IdPersona { get; set; }
        public virtual Persona Persona { get; set; }

        [ForeignKey("Obra")]
        public int IdObra { get; set; }
        public virtual Obra Obra { get; set; }

        public DateTime FechaLike { get; set; } = DateTime.Now;
    }
}

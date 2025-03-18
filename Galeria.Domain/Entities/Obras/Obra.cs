using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Comentarios;
using Galeria.Domain.Entities.Likes;
using Galeria.Domain.Entities.Usuarios.Artistas;

namespace Galeria.Domain.Entities.Obras
{
    [Table("Tbl_Obras")]
    public class Obra : BaseEntity
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey("Artista")]
        public int IdArtista { get; set; }
        public virtual Artista Artista { get; set; }

        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<ObraEnExposicion> Exposiciones { get; set; }
        public virtual ICollection<ObraCategoria> ObrasCategorias { get; set; }

    }
}

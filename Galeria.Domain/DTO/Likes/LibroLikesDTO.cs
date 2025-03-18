namespace Galeria.Domain.DTO.Likes;
public class LibroLikesDTO
{
    public int LibroId { get; set; }
    public int TotalLikes { get; set; }
    public string TituloLibro { get; set; }
    public List<string> UsuariosQueDieronLike { get; set; }
}


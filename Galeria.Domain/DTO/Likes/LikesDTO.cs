namespace Galeria.Domain.DTO.Likes;
public class LikesDTO
{
    public int IdObra { get; set; }
    public int TotalLikes { get; set; }
    public List<string> UsuariosQueDieronLike { get; set; }
}


namespace Galeria.Domain.DTO.Likes
{
    public class LikeDTO : BaseDTO
    {
        public int IdPersona { get; set; }
        public int IdObra { get; set; }
        public DateTime FechaLike { get; set; }
    }
}

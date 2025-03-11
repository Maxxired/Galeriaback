namespace Galeria.Domain.DTO
{
    public abstract class BaseDTO
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }

}

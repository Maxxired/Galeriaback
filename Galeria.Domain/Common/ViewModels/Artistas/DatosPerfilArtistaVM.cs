namespace Galeria.Domain.Common.ViewModels.Artistas
{
    public class DatosPerfilArtistaVM
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string AvatarURL { get; set; }
        public string? idApplicationUser { get; set; }
    }
}

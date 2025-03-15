using Galeria.Domain.DTO.Usuarios.Personas;

namespace Galeria.Domain.DTO.Usuarios.Artistas
{
    public class ArtistaDTO : PersonaDTO
    {
        public string Pais { get; set; }
        public string Biografia { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
}

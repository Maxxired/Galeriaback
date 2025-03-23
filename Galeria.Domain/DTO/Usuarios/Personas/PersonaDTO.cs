using Galeria.Domain.DTO;
using static Galeria.Domain.Common.Util.Enums;

namespace Galeria.Domain.DTO.Usuarios.Personas
{
    public class PersonaDTO: BaseDTO
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string IdApplicationUser { get; set; }
    }
}

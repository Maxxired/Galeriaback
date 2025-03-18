using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galeria.Domain.DTO.Usuarios.Artistas
{
    public class RegistrarArtistaDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El país es obligatorio")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "La biografía es obligatoria")]
        public string Biografia { get; set; }

        public DateTime? FechaNacimiento { get; set; }
    }

}

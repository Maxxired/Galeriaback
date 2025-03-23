using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Domain.DTO.Auth;

namespace Galeria.Domain.DTO.Usuarios.Artistas
{
    public class RegistrarArtistaDTO : UserDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellidos { get; set; }
    }

}

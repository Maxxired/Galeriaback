using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Galeria.Domain.Entities.Obras;
using Galeria.Domain.Entities.Usuarios.Personas;

namespace Galeria.Domain.Entities.Usuarios.Artistas
{
    [Table("Tbl_Artistas")]
    public class Artista : Persona
    {
        public string Pais { get; set; }
        public string Biografia { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public virtual ICollection<Obra> Obras { get; set; }
    }
}

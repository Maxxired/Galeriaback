using Microsoft.AspNetCore.Http;

namespace Galeria.Domain.Common.ViewModels.Util
{
    public class FormImagenesVM
    {
        public int IdServicio { get; set; }
        public IFormFile Archivo { get; set; }
    }

    public class EditImagenesVM
    {
        public int IdImagenServicio { get; set; }
        public IFormFile Archivo { get; set; }
    }

    public class FormImagenesEmpresaVM
    {
        public int IdEmpresa { get; set; }
        public IFormFile Archivo { get; set; }
    }

    public class FormImagenPerfilVM
    {
        public string IdApplicationUser { get; set; }
        public IFormFile Archivo { get; set; }
    }

}

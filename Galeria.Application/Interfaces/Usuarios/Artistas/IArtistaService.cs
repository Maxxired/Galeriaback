using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galeria.Application.Interfaces.Base;
using Galeria.Domain.Common.ViewModels.Artistas;
using Galeria.Domain.Common.ViewModels.Personas;
using Galeria.Domain.Common.ViewModels.Util;
using Galeria.Domain.DTO.Usuarios.Artistas;
using Galeria.Domain.Entities.Usuarios.Artistas;
using Microsoft.AspNetCore.Http;

namespace Galeria.Application.Interfaces.Usuarios.Artistas
{
    public interface IArtistaService : IServiceBase<Artista, ArtistaDTO>
    {
        Task<ResponseHelper> ActualizarPerfilUsuario(string idApplicationUser, ActualizarPerfilArtistaVM datos);
        Task<ResponseHelper> SubirFotoPerfil(string idApplicationUser, IFormFile archivo);
        Task<ResponseHelper> ActualizarFotoPerfil(string idApplicationUser, IFormFile archivo);
        Task<ResponseHelper> EliminarFotoPerfil(string idApplicationUser);
        Task<ResponseHelper> GetDatosPerfil(string idApplicationUser);
    }
}

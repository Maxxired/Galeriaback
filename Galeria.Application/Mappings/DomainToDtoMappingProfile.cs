using AutoMapper;
using Galeria.Domain.Entities;
using Galeria.Domain.DTO;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Domain.DTO.Logs;
using Galeria.Domain.Entities.Logs;
using Galeria.Domain.DTO.Usuarios.Personas;
using Galeria.Domain.Entities.Usuarios.Artistas;
using Galeria.Domain.DTO.Usuarios.Artistas;
using Galeria.Domain.Entities.Obras;
using Galeria.Domain.DTO.Obras;
using Galeria.Domain.Entities.Exposiciones;
using Galeria.Domain.DTO.Exposiciones;
using Galeria.Domain.Entities.Comentarios;
using Galeria.Domain.DTO.Comentarios;
using Galeria.Domain.Entities.Likes;
using Galeria.Domain.DTO.Likes;
using Galeria.Domain.Entities.Categorias;
using Galeria.Domain.DTO.Categorias;

namespace Galeria.Application.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            this.CreateMap<BaseEntity, BaseDTO>();
            this.CreateMap<PersonaDTO, Persona>().ReverseMap();
            this.CreateMap<LogAction, LogActionDTO>().ReverseMap();
            this.CreateMap<LogError, LogErrorDTO>().ReverseMap();
            this.CreateMap<Artista, ArtistaDTO>().ReverseMap();
            this.CreateMap<Exposicion, ExposicionDTO>().ReverseMap();
            this.CreateMap<ObraEnExposicion, ObraEnExposicionDTO>().ReverseMap();
            this.CreateMap<Comentario, ComentarioDTO>().ReverseMap();
            this.CreateMap<Like, LikeDTO>().ReverseMap();
            this.CreateMap<Obra, ObraDTO>().ReverseMap();
            this.CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            this.CreateMap<ObraCategoria, ObraCategoriaDTO>().ReverseMap();
        }
    }
}

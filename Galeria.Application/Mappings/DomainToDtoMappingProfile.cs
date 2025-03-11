using AutoMapper;
using Galeria.Domain.Entities;
using Galeria.Domain.DTO;
using Galeria.Domain.DTO.Usuarios.Personas;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Domain.DTO.Logs;
using Galeria.Domain.Entities.Logs;

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
        }
    }
}

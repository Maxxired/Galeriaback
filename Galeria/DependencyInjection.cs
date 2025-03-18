using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Galeria.Infraestructure;
using Galeria.Application.Mappings;
using Galeria.Application.Interfaces.Auth;
using Galeria.Application.Services.Auth;
using Galeria.Infraestructure.Repositories.Auth;
using Galeria.Application.Interfaces.Usuarios.Personas;
using Galeria.Application.Services.Usuarios.Personas;
using Galeria.Infraestructure.Repositories.Usuarios.Personas;
using Galeria.Infraestructure.Interfaces.Usuarios.Personas;
using Galeria.Infraestructure.Interfaces.Auth;
using Galeria.Application.Services.Seeders;
using Galeria.Application.Interfaces.Logs;
using Galeria.Application.Services.Logs;
using Galeria.Infraestructure.Interfaces.Log;
using Galeria.Infraestructure.Repositories.Log;
using Galeria.Application.Interfaces.Likes;
using Galeria.Application.Services.Likes;
using Galeria.Application.Interfaces.Comentarios;
using Galeria.Application.Services.Comentarios;
using Galeria.Application.Interfaces.Exposiciones;
using Galeria.Application.Services.Exposiciones;
using Galeria.Application.Interfaces.Categorias;
using Galeria.Application.Services.Categorias;
using Galeria.Infraestructure.Interfaces.Obras;
using Galeria.Application.Services.Obras;
using Galeria.Application.Interfaces.Obras;
using Galeria.Domain.Entities.Obras;
using Galeria.Application.Interfaces.Usuarios.Artistas;
using Galeria.Application.Services.Usuarios.Artistas;
using Galeria.Infraestructure.Interfaces.Likes;
using Galeria.Infraestructure.Repositories.Likes;
using Galeria.Infraestructure.Interfaces.Comentarios;
using Galeria.Infraestructure.Repositories.Comentarios;
using Galeria.Infraestructure.Repositories.Esposiciones;
using Galeria.Infraestructure.Interfaces.Esposiciones;
using Galeria.Infraestructure.Interfaces.Categorias;
using Galeria.Infraestructure.Repositories.Categorias;
using Galeria.Infraestructure.Repositories.Obras;
using Galeria.Infraestructure.Interfaces.Usuarios.Artistas;
using Galeria.Infraestructure.Repositories.Usuarios.Artistas;

namespace Galeria.WebAPI
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the dependency injection.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            #region DataBaseConnection
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            #endregion

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToDtoMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            AddServices(services);
            AddRepository(services);

            return services;
        }

        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPersonaService, PersonaService>();
            services.AddScoped<ILogActionService, LogActionService>();
            services.AddScoped<ILogErrorService, LogErrorService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IComentarioService, ComentarioService>();
            services.AddScoped<IExposicionService, ExposicionService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IObraCategoriaService, ObraCategoriaService>();
            services.AddScoped<IObraEnExposicionService, ObraEnExposicionService>();
            services.AddScoped<IObraService, ObraService>();
            services.AddScoped<IArtistaService, ArtistaService>();
            services.AddScoped<Seed>();
        }

        /// <summary>
        /// Adds the repository.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void AddRepository(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<ILogActionRepository, LogActionRepository>();
            services.AddScoped<ILogErrorRepository, LogErrorRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IComentarioRepository, ComentarioRepository>();
            services.AddScoped<IExposicionRepository, ExposicionRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IObraCategoriaRepository, ObraCategoriaRepository>();
            services.AddScoped<IObraEnExposicionRepository, ObraEnExposicionRepository>();
            services.AddScoped<IObraRepository, ObraRepository>();
            services.AddScoped<IArtistaRepository, ArtistaRepository>();
        }
    }
}

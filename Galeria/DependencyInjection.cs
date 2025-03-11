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
        }
    }
}

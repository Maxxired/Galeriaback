using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Galeria.Domain.Entities;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Domain.Entities.Logs;

namespace Galeria.Infraestructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<LogAction> LogActions { get; set; }
        public DbSet<LogError> LogErrors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Persona>()
                .HasOne(p => p.ApplicationUser)
                .WithMany()
                .HasForeignKey(p => p.IdApplicationUser)
                .IsRequired(false);

            var entitiesAssembly = typeof(BaseEntity).Assembly;
            modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        }
    }
}

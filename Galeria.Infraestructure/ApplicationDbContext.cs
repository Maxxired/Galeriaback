using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Galeria.Domain.Entities;
using Galeria.Domain.Entities.Usuarios.Personas;
using Galeria.Domain.Entities.Logs;
using Galeria.Domain.Entities.Categorias;
using Galeria.Domain.Entities.Comentarios;
using Galeria.Domain.Entities.Exposiciones;
using Galeria.Domain.Entities.Likes;
using Galeria.Domain.Entities.Obras;
using Galeria.Domain.Entities.Usuarios.Artistas;

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
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Exposicion> Exposiciones { get; set; }
        public DbSet<ObraEnExposicion> ObrasEnExposicion { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ObraCategoria> ObrasCategorias { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Persona>()
                .HasOne(p => p.ApplicationUser)
                .WithMany()
                .HasForeignKey(p => p.IdApplicationUser)
                .IsRequired(false);
            // Relación Artista - Obra (Uno a Muchos)
            modelBuilder.Entity<Obra>()
                .HasOne(o => o.Artista)
                .WithMany(a => a.Obras)
                .HasForeignKey(o => o.IdArtista)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Obra - Comentarios (Uno a Muchos)
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Obra)
                .WithMany(o => o.Comentarios)
                .HasForeignKey(c => c.IdObra)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Persona - Comentarios (Uno a Muchos)
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Persona)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.IdPersona)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Persona - Likes (Uno a Muchos)
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Persona)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.IdPersona)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Obra - Likes (Uno a Muchos)
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Obra)
                .WithMany(o => o.Likes)
                .HasForeignKey(l => l.IdObra)
                .OnDelete(DeleteBehavior.Restrict);

            // Clave compuesta para Likes (Cada persona solo puede dar un like por obra)
            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.IdPersona, l.IdObra });

            // Índice único en Likes (Cada persona solo puede dar un like por obra)
            modelBuilder.Entity<Like>()
                .HasIndex(l => new { l.IdPersona, l.IdObra })
                .IsUnique();

            // Relación Obra - Exposición (Muchos a Muchos)
            modelBuilder.Entity<ObraEnExposicion>()
                .HasKey(oe => new { oe.IdObra, oe.IdExposicion });

            modelBuilder.Entity<ObraEnExposicion>()
                .HasOne(oe => oe.Obra)
                .WithMany(o => o.Exposiciones)
                .HasForeignKey(oe => oe.IdObra)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ObraEnExposicion>()
                .HasOne(oe => oe.Exposicion)
                .WithMany(e => e.Obras)
                .HasForeignKey(oe => oe.IdExposicion)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Obra - Categoría (Muchos a Muchos)
            modelBuilder.Entity<ObraCategoria>()
                .HasKey(oc => new { oc.IdObra, oc.IdCategoria });

            modelBuilder.Entity<ObraCategoria>()
                .HasOne(oc => oc.Obra)
                .WithMany(o => o.ObrasCategorias)
                .HasForeignKey(oc => oc.IdObra)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ObraCategoria>()
                .HasOne(oc => oc.Categoria)
                .WithMany(c => c.ObrasCategorias)
                .HasForeignKey(oc => oc.IdCategoria)
                .OnDelete(DeleteBehavior.Cascade);

            var entitiesAssembly = typeof(BaseEntity).Assembly;
            modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        }
    }
}

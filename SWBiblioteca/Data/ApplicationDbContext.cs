using Microsoft.EntityFrameworkCore;
using SWBiblioteca.Entity;
using SWBiblioteca.Entity.Procedure;
using SWBiblioteca.Models;
using System.Reflection.Emit;

namespace SWBiblioteca.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            //builder.Entity<entidad>().ToTable("nombretabla", "esquema");
            model.Entity<TIPO_PERSONA>().HasIndex(z => z.IdTipoPersona).IsUnique();
            model.Entity<CATEGORIA>().HasIndex(z => z.IdCategoria).IsUnique();
            model.Entity<ESTADOS>().HasIndex(z => z.Id_Estado).IsUnique();
            model.Entity<EDITORIAL>().HasIndex(z => z.IdEditorial).IsUnique();
            model.Entity<AUTOR>().HasIndex(z => z.IdAutor).IsUnique();
            model.Entity<LIBRO>().HasIndex(z => z.IdLibro).IsUnique();
            model.Entity<LISTAR_LIBROS_Q01>().HasIndex(z => z.IdLibro).IsUnique();
            model.Entity<PERSONA>().HasIndex(z => z.IdPersona).IsUnique();
            model.Entity<PR_PERSONA_Q01>().HasIndex(z => z.IdPersona).IsUnique();
            model.Entity<LOG_PERSONA>().HasIndex(z => z.Id_LogPersona).IsUnique();
            model.Entity<CONSULTAR_PERFIL_Q01>().HasIndex(z => z.Codigo).IsUnique();
        }
        public DbSet<PR_PERSONA_Q01> PR_PERSONA_Q01 { get; set; }
        public DbSet<TIPO_PERSONA> TIPO_PERSONA { get; set; }
        public DbSet<CATEGORIA> CATEGORIA { get; set; }
        public DbSet<ESTADOS> ESTADOS { get; set; }
        public DbSet<EDITORIAL> EDITORIAL { get; set; }
        public DbSet<AUTOR> AUTOR { get; set; }
        public DbSet<LIBRO> LIBRO { get; set; }
        public DbSet<LISTAR_LIBROS_Q01> LISTAR_LIBROS_Q01 { get; set; }
        public DbSet<PERSONA> PERSONA { get; set; }
        public DbSet<LOG_PERSONA> LOG_PERSONA { get; set; }
        public DbSet<CONSULTAR_PERFIL_Q01> CONSULTAR_PERFIL_Q01 { get; set; }
    }
}

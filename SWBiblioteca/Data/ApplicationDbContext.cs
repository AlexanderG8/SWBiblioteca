using Microsoft.EntityFrameworkCore;
using SWBiblioteca.Entity;
using SWBiblioteca.Entity.Procedure;
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

        }

        public DbSet<TIPO_PERSONA> TIPO_PERSONA { get; set; }
        public DbSet<CATEGORIA> CATEGORIA { get; set; }
        public DbSet<ESTADOS> ESTADOS { get; set; }
        public DbSet<EDITORIAL> EDITORIAL { get; set; }
        public DbSet<AUTOR> AUTOR { get; set; }
        public DbSet<LIBRO> LIBRO { get; set; }
        public DbSet<LISTAR_LIBROS_Q01> LISTAR_LIBROS_Q01 { get; set; }

    }
}

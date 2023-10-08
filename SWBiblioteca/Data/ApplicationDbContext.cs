using Microsoft.EntityFrameworkCore;
using SWBiblioteca.Entity;
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
        }

        public DbSet<TIPO_PERSONA> TIPO_PERSONA { get; set; }

    }
}

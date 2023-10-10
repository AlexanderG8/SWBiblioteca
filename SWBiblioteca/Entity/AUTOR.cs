using System.ComponentModel.DataAnnotations;

namespace SWBiblioteca.Entity
{
    public class AUTOR
    {
        [Key]
        public int IdAutor { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

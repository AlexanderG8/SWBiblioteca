using System.ComponentModel.DataAnnotations;

namespace SWBiblioteca.Entity
{
    public class EDITORIAL
    {
        [Key]
        public int IdEditorial { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

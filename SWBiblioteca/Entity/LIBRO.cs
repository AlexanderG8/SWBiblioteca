using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWBiblioteca.Entity
{
    public class LIBRO
    {
        [Key]
        public int IdLibro { get; set; }
        public string? Titulo { get; set; }
        public string? RutaPortada { get; set; }
        public string? NombrePortada { get; set; }
        public int IdAutor { get; set; }
        public int IdCategoria { get; set; }
        public int IdEditorial { get; set; }
        public string? Ubicacion { get; set; }
        public int Ejemplares { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        [NotMapped]
        public IFormFile Imagen { get; set; }
    }
}

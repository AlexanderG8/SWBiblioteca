using System.ComponentModel.DataAnnotations;

namespace SWBiblioteca.Entity.Procedure
{
    public class LISTAR_LIBROS_Q01
    {
        [Key]
        public int IdLibro { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Categoria { get; set; }
        public string? Editorial { get; set; }
        public string? Ubicacion { get; set; }
        public int Ejemplares { get; set; }
        public bool Estado { get; set; }
    }
}

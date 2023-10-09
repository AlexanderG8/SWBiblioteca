using System.ComponentModel.DataAnnotations;

namespace SWBiblioteca.Entity
{
    public class ESTADOS
    {
        [Key]
        public int Id_Estado { get; set; }
        public bool Valor { get; set; }
        public string? Descripcion { get; set; }
    }
}

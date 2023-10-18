using System.ComponentModel.DataAnnotations;

namespace SWBiblioteca.Models
{
    public class TIPO_PERSONA
    {
        [Key]
        public int IdTipoPersona { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

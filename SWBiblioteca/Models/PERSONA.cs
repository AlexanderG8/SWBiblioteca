using System.ComponentModel.DataAnnotations;

namespace SWBiblioteca.Models
{
    public class PERSONA
    {
        [Key]
        public int IdPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
        public string? Codigo { get; set; }
        public int IdTipoPersona { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

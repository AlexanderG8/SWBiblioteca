using System.ComponentModel.DataAnnotations;

namespace SWBiblioteca.Entity.Procedure
{
    public class PR_PERSONA_Q01
    {
        [Key]
        public int IdPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Codigo { get; set; }
        public string? TipoPersona { get; set; }
        public string? Estado { get; set; }
    }
}

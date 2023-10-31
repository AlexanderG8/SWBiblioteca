using System.ComponentModel.DataAnnotations;

namespace SWBiblioteca.Models
{
    public class LOG_PERSONA
    {
        [Key]
        public int Id_LogPersona { get; set; }
        public string? Cod_Usuario { get; set; }
        public string? Cod_Validador { get; set; }
        public string? Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public TimeSpan HoraRegistro { get; set; }
    }
}

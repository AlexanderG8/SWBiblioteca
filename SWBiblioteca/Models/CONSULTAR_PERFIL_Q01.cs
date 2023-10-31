using System.ComponentModel.DataAnnotations;

namespace SWBiblioteca.Models
{
    public class CONSULTAR_PERFIL_Q01
    {
        [Key]
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
    }
}

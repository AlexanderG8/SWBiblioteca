﻿using System.ComponentModel.DataAnnotations;

namespace SWBiblioteca.Entity
{
    public class CATEGORIA
    {
        [Key]
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

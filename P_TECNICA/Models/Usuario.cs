using System;
using System.Collections.Generic;

namespace P_TECNICA.Models
{
    public partial class Usuario
    {
        public uint IdUsuarios { get; set; }
        public string? Username { get; set; }
        public string? Pass { get; set; }
        public string? Nombre { get; set; }
        public DateTime? Fnacimiento { get; set; }
        public string? Email { get; set; }
        public string? ConfirmarClave { get; set; }
    }
}

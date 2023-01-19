using System;
using System.Collections.Generic;

namespace P_TECNICA.Models
{
    public partial class Empleado
    {
        public uint idEmpleados { get; set; }
        public string? NombreE { get; set; }
        public string? DPI { get; set; }
        public int? Hijos { get; set; }
        public string? SalarioB { get; set; }
        public int? userUdpdate { get; set; }
        public DateTime? fechaUpdate { get; set; }
    }
}
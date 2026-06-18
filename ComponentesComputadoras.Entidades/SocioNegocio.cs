using ComponentesComputadoras.Abstraccioness;
using ComponentesComputadoras.Enumeraciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentesComputadoras.Entities
{
    public class SocioNegocio : IEntidad
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; } // Cliente o Proveedor

        // Relaciones opcionales
        public int? ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public int? ProveedorId { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}

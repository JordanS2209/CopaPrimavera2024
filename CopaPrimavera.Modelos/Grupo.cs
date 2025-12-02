using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class Grupo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TorneoId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Nombre { get; set; } = null!; // "A", "B", ...

        // Orden para generar fixture
        public int Orden { get; set; }

        // Equipos asignados al grupo
        public List<Equipo>? Equipos { get; set; }
    }
}

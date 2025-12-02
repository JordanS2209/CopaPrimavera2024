using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class Partido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TorneoId { get; set; }

        public RondaPartido Ronda { get; set; } = RondaPartido.Grupo;

        [MaxLength(10)]
        public string? NombreGrupo { get; set; } // "A", "B", ... si aplica

        [Required]
        public int EquipoLocalId { get; set; }

        [Required]
        public int EquipoVisitanteId { get; set; }

        public DateTime ProgramadoParaUtc { get; set; }

        // Resultado (null si no jugado)
        public int? GolesLocal { get; set; }
        public int? GolesVisitante { get; set; }

        public EstadoPartido Estado { get; set; } = EstadoPartido.Programado;

        
        

        // Relaciones
        public List<Gol> Goles { get; set; }
        public List<Tarjeta> Tarjetas { get; set; } 
    }
}

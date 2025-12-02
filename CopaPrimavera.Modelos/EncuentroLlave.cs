using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class EncuentroLlave
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TorneoId { get; set; }
        public Torneo Torneo { get; set; } = null!;

        public RondaPartido Ronda { get; set; }

        // Los equipos pueden venir directamente o desde el ganador de otro encuentro
        public int? EquipoLocalId { get; set; }
        public Equipo? EquipoLocal { get; set; }

        public int? EquipoVisitanteId { get; set; }
        public Equipo? EquipoVisitante { get; set; }

        // Referencia al encuentro cuyo ganador será el equipo local (si aplica)
        public int? GanadorDeEncuentroLocalId { get; set; }
        public EncuentroLlave? EncuentroGanadorLocal { get; set; }

        // Referencia al encuentro cuyo ganador será el equipo visitante (si aplica)
        public int? GanadorDeEncuentroVisitanteId { get; set; }
        public EncuentroLlave? EncuentroGanadorVisitante { get; set; }

        // Partido asociado si ya se programó el partido real (opcional)
        public int? PartidoId { get; set; }

        // Orden/posición dentro de la ronda
        public int Orden { get; set; }
    }
}


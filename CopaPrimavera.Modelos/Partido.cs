using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public enum FasePartido { Grupo, Octavos, Cuartos, Semifinal, Final }
    public class Partido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TorneoId { get; set; }
        public Torneo? Torneo { get; set; }

        public int? GrupoId { get; set; }

        public FasePartido Fase { get; set; } = FasePartido.Grupo;

        public int EquipoLocalId { get; set; }
        public Equipo? EquipoLocal { get; set; }

        public int EquipoVisitanteId { get; set; }
        public Equipo? EquipoVisitante { get; set; }

        public DateTime Programado { get; set; }

        public int? GolesLocal { get; set; }
        public int? GolesVisitante { get; set; }

        // En eliminatoria se usan penales si empatan
        public int? PenalesLocal { get; set; }
        public int? PenalesVisitante { get; set; }

        public bool IsJugado { get; set; } = false;

        // GanadorId set cuando IsJugado 
        public int? GanadorId { get; set; }
        public List<Gol>? Goles { get; set; }
        public List<Tarjeta>? Tarjetas { get; set; }
    }
}

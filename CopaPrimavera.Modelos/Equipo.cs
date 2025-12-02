using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class Equipo
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int TorneoId { get; set; }

        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; } = null!;

        // Semilla/posición para sorteos/emparejamientos
        public int Semilla { get; set; } = 0;

        // Relación a grupo (si aplica)
        public int? GrupoId { get; set; }

        // Estadísticas acumuladas (opcionales, pueden recalcularse)
        public int Puntos { get; set; }
        public int GolesAFavor { get; set; }
        public int GolesEnContra { get; set; }
        public int PartidosJugados { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }

        // Relaciones
        public List<Jugador>? Jugadores { get; set; } 
    }
}

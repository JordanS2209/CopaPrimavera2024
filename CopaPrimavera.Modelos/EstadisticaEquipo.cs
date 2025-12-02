using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class EstadisticaEquipo
    {
        public int Id { get; set; } 
        public int EquipoId { get; set; }
        public Equipo Equipo { get; set; } = null!;

        public int Puntos { get; set; }
        public int Juegos { get; set; }
        public int Victorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int GolesAFavor { get; set; }
        public int GolesEnContra { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }
    }
}

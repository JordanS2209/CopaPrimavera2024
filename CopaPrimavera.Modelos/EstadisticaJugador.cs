using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class EstadisticaJugador
    {
        public int Id { get; set; }
        public int JugadorId { get; set; }
        public Jugador Jugador { get; set; } = null!;

        public int Goles { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }
    }
}

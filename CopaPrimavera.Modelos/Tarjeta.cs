using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class Tarjeta
    {
        [Key]
        public int Id { get; set; }

        public int PartidoId { get; set; }
        public Partido? Partido { get; set; } 

        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; } 

        public int Minuto { get; set; }
    }
}

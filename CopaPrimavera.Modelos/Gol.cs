using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class Gol
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PartidoId { get; set; }
        public Partido Partido { get; set; } = null!;

        public int? JugadorId { get; set; }
        public Jugador? Jugador { get; set; }

        // Minuto del gol
        public int Minuto { get; set; }

        // Indica autogol si aplica
        public bool Autogol { get; set; } = false;
    }
}


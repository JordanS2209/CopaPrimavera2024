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
        public Guid Id { get; set; }

        [Required]
        public Guid PartidoId { get; set; }
        public Partido Partido { get; set; } = null!;

        public Guid? JugadorId { get; set; }
        public Jugador? Jugador { get; set; }

        // Minuto del gol
        public int Minuto { get; set; }

        // Indica autogol si aplica
        public bool Autogol { get; set; } = false;
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class Suspension
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int JugadorId { get; set; }
        public Jugador? Jugador { get; set; }

        public string Motivo { get; set; } 

        public int? UntilPartidoId { get; set; }

        public DateTime? UntilDate { get; set; }

        public DateTime? CreadoEn { get; set; } = DateTime.UtcNow;
    }
}

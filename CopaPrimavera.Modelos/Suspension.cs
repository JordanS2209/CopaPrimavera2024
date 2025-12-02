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
        public Guid Id { get; set; }

        [Required]
        public Guid JugadorId { get; set; }
        public Jugador Jugador { get; set; } = null!;

        [MaxLength(200)]
        public string Motivo { get; set; } = null!;

        // Suspensión hasta partido concreto (si aplica)
        public Guid? UntilPartidoId { get; set; }

        // O hasta fecha
        public DateTime? UntilDate { get; set; }

        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
    }
}

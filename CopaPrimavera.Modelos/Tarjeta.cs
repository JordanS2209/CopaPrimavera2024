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
        public Guid Id { get; set; }

        [Required]
        public Guid PartidoId { get; set; }
        public Partido Partido { get; set; } = null!;

        [Required]
        public Guid JugadorId { get; set; }
        public Jugador Jugador { get; set; } = null!;

        [Required]
        public TipoTarjeta Tipo { get; set; }

        public int Minuto { get; set; }
    }
}

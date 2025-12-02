using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CopaPrimavera.Modelos
{
    public class Torneo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = null!;

        [Required]
        public TipoTorneo Tipo { get; set; }

        public EstadoTorneo Estado { get; set; } = EstadoTorneo.Borrador;

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public int MinEquipos { get; set; } = 8;
        public int MaxEquipos { get; set; } = 32;

        public DateTime CreadoEnUtc { get; set; } = DateTime.UtcNow;

        // Relaciones
        public List<Equipo>? Equipos { get; set; } 
        public List<Grupo>? Grupos { get; set; } 
        public List<Partido>? Partidos { get; set; }
        public List<EncuentroLlave>? EncuentrosLlave { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CopaPrimavera.Modelos
{
    public class Torneo
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Tipo { get; set; } //Liga, Copa, Mixto

        public int MinEquipos { get; set; } = 8;
        public int MaxEquipos { get; set; } = 32;

        public DateTime CreadoEnUtc { get; set; } = DateTime.UtcNow;

        
        public List<Equipo>? Equipos { get; set; } 
        public List<Grupo>? Grupos { get; set; } 
        public List<Partido>? Partidos { get; set; }
        public List<EncuentroLlave>? EncuentrosLlave { get; set; }
    }
}

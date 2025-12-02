using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CopaPrimavera.Modelos
{

    public enum TipoTorneo { Liga, Copa, Mixto }
    public enum EstadoTorneo { Planificado, Iniciado, Finalizado }
    public class Torneo
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public TipoTorneo Tipo { get; set; }

        public EstadoTorneo Estado { get; set; } = EstadoTorneo.Planificado;

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public int MinEquipos { get; set; } = 8;
        public int MaxEquipos { get; set; } = 32;

        public DateTime CreadoEnUtc { get; set; } = DateTime.UtcNow;

        public List<Equipo>? Equipos { get; set; } = new();
        public List<Grupo>? Grupos { get; set; }
        public List<Partido>? Partidos { get; set; }
        public List<EncuentroLlave>? EncuentrosLlave { get; set; }
    }
}

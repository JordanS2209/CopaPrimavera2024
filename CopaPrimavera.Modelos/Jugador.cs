using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class Jugador
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int EquipoId { get; set; }
        public Equipo? Equipo { get; set; }
        public int TorneoId { get; set; }

        public int Goles { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class Partido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TorneoId { get; set; }

        public int GrupoId{ get; set; } 

        public int EquipoLocalId { get; set; }
        public int EquipoVisitanteId { get; set; }

        public DateTime Programado { get; set; }

        public int? GolesLocal { get; set; }
        public int? GolesVisitante { get; set; }
   
        public List<Gol> Goles { get; set; }
        public List<Tarjeta> Tarjetas { get; set; } 
    }
}

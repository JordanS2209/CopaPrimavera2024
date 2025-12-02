using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class Equipo
    {

        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? JugadorId { get; set; }
        public List<Jugador>? Jugadores { get; set; } 
    }
}

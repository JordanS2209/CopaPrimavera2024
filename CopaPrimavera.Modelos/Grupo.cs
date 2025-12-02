using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class Grupo
    {
        [Key]
        public int Id { get; set; }

        public int TorneoId { get; set; }

        public string Nombre { get; set; }

 
    }
}

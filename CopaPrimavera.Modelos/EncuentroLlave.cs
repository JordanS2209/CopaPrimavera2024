using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public class EncuentroLlave
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TorneoId { get; set; }
        public Torneo? Torneo { get; set; }


       
    }
}


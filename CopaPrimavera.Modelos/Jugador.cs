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

        [Required]
        public int EquipoId { get; set; }

        [Required]
        public int TorneoId { get; set; }

        [Required]
        [MaxLength(140)]
        public string Nombre { get; set; } = null!;

        // Identificador externo opcional (DNI, documento)
        [MaxLength(80)]
        public string? IdentificadorExterno { get; set; }

        // Estadísticas
        public int Goles { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaPrimavera.Modelos
{
    public enum TipoTorneo
    {
        Liga,
        Copa,
        Mixto
    }

    public enum EstadoTorneo
    {
        Borrador,
        Iniciado,
        Finalizado
    }

    public enum EstadoPartido
    {
        Programado,
        Jugado,
        Cancelado
    }

    public enum RondaPartido
    {
        Grupo,
        Octavos,
        Cuartos,
        Semifinal,
        Final
    }

    public enum TipoTarjeta
    {
        Amarilla,
        Roja
    }
}
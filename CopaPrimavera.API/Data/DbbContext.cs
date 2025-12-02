using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CopaPrimavera.Modelos;

    public class DbbContext : DbContext
    {
        public DbbContext (DbContextOptions<DbbContext> options)
            : base(options)
        {
        }

        public DbSet<CopaPrimavera.Modelos.EncuentroLlave> EncuentrosLlaves { get; set; } = default!;

        public DbSet<CopaPrimavera.Modelos.Equipo> Equipos { get; set; } = default!;

        public DbSet<CopaPrimavera.Modelos.EstadisticaEquipo> EstadisticasEquipos { get; set; } = default!;

        public DbSet<CopaPrimavera.Modelos.EstadisticaJugador> EstadisticasJugadores { get; set; } = default!;

        public DbSet<CopaPrimavera.Modelos.Gol> Goles { get; set; } = default!;

        public DbSet<CopaPrimavera.Modelos.Grupo> Grupos { get; set; } = default!;

        public DbSet<CopaPrimavera.Modelos.Jugador> Jugadores { get; set; } = default!;

        public DbSet<CopaPrimavera.Modelos.Partido> Partidos { get; set; } = default!;

        public DbSet<CopaPrimavera.Modelos.Suspension> Suspensiones { get; set; } = default!;

        public DbSet<CopaPrimavera.Modelos.Tarjeta> Tarjetas { get; set; } = default!;

        public DbSet<CopaPrimavera.Modelos.Torneo> Torneos { get; set; } = default!;
    }

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CopaPrimavera.API.Migrations
{
    /// <inheritdoc />
    public partial class v01Postgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Torneos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MinEquipos = table.Column<int>(type: "integer", nullable: false),
                    MaxEquipos = table.Column<int>(type: "integer", nullable: false),
                    CreadoEnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EncuentrosLlaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TorneoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuentrosLlaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncuentrosLlaves_Torneos_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    TorneoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipos_Torneos_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TorneoId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupos_Torneos_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstadisticasEquipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EquipoId = table.Column<int>(type: "integer", nullable: false),
                    Puntos = table.Column<int>(type: "integer", nullable: false),
                    Juegos = table.Column<int>(type: "integer", nullable: false),
                    Victorias = table.Column<int>(type: "integer", nullable: false),
                    Empates = table.Column<int>(type: "integer", nullable: false),
                    Derrotas = table.Column<int>(type: "integer", nullable: false),
                    GolesAFavor = table.Column<int>(type: "integer", nullable: false),
                    GolesEnContra = table.Column<int>(type: "integer", nullable: false),
                    TarjetasAmarillas = table.Column<int>(type: "integer", nullable: false),
                    TarjetasRojas = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadisticasEquipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadisticasEquipos_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    EquipoId = table.Column<int>(type: "integer", nullable: false),
                    TorneoId = table.Column<int>(type: "integer", nullable: false),
                    Goles = table.Column<int>(type: "integer", nullable: false),
                    TarjetasAmarillas = table.Column<int>(type: "integer", nullable: false),
                    TarjetasRojas = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jugadores_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TorneoId = table.Column<int>(type: "integer", nullable: false),
                    GrupoId = table.Column<int>(type: "integer", nullable: true),
                    Fase = table.Column<int>(type: "integer", nullable: false),
                    EquipoLocalId = table.Column<int>(type: "integer", nullable: false),
                    EquipoVisitanteId = table.Column<int>(type: "integer", nullable: false),
                    Programado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GolesLocal = table.Column<int>(type: "integer", nullable: true),
                    GolesVisitante = table.Column<int>(type: "integer", nullable: true),
                    PenalesLocal = table.Column<int>(type: "integer", nullable: true),
                    PenalesVisitante = table.Column<int>(type: "integer", nullable: true),
                    IsJugado = table.Column<bool>(type: "boolean", nullable: false),
                    GanadorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidos_Equipos_EquipoLocalId",
                        column: x => x.EquipoLocalId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partidos_Equipos_EquipoVisitanteId",
                        column: x => x.EquipoVisitanteId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partidos_Torneos_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstadisticasJugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JugadorId = table.Column<int>(type: "integer", nullable: false),
                    Goles = table.Column<int>(type: "integer", nullable: false),
                    TarjetasAmarillas = table.Column<int>(type: "integer", nullable: false),
                    TarjetasRojas = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadisticasJugadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadisticasJugadores_Jugadores_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Jugadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suspensiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JugadorId = table.Column<int>(type: "integer", nullable: false),
                    Motivo = table.Column<string>(type: "text", nullable: false),
                    UntilPartidoId = table.Column<int>(type: "integer", nullable: true),
                    UntilDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suspensiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suspensiones_Jugadores_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Jugadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartidoId = table.Column<int>(type: "integer", nullable: false),
                    JugadorId = table.Column<int>(type: "integer", nullable: true),
                    Minuto = table.Column<int>(type: "integer", nullable: false),
                    Autogol = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goles_Jugadores_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Jugadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goles_Partidos_PartidoId",
                        column: x => x.PartidoId,
                        principalTable: "Partidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    PartidoId = table.Column<int>(type: "integer", nullable: false),
                    JugadorId = table.Column<int>(type: "integer", nullable: false),
                    Minuto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarjetas_Jugadores_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Jugadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarjetas_Partidos_PartidoId",
                        column: x => x.PartidoId,
                        principalTable: "Partidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncuentrosLlaves_TorneoId",
                table: "EncuentrosLlaves",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_TorneoId",
                table: "Equipos",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadisticasEquipos_EquipoId",
                table: "EstadisticasEquipos",
                column: "EquipoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstadisticasJugadores_JugadorId",
                table: "EstadisticasJugadores",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Goles_JugadorId",
                table: "Goles",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Goles_PartidoId",
                table: "Goles",
                column: "PartidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_TorneoId",
                table: "Grupos",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jugadores_EquipoId",
                table: "Jugadores",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoLocalId",
                table: "Partidos",
                column: "EquipoLocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoVisitanteId",
                table: "Partidos",
                column: "EquipoVisitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_TorneoId",
                table: "Partidos",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_Suspensiones_JugadorId",
                table: "Suspensiones",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjetas_JugadorId",
                table: "Tarjetas",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjetas_PartidoId",
                table: "Tarjetas",
                column: "PartidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncuentrosLlaves");

            migrationBuilder.DropTable(
                name: "EstadisticasEquipos");

            migrationBuilder.DropTable(
                name: "EstadisticasJugadores");

            migrationBuilder.DropTable(
                name: "Goles");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Suspensiones");

            migrationBuilder.DropTable(
                name: "Tarjetas");

            migrationBuilder.DropTable(
                name: "Jugadores");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Torneos");
        }
    }
}

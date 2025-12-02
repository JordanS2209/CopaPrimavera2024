using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using CopaPrimavera.Modelos;
using System.Text.Json.Serialization;

namespace CopaPrimavera.ApiTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7230/");

            // 1. Crear torneo tipo "Mixto"
            var torneo = new Torneo
            {
                Nombre = "Copa Primavera 2024",
                Tipo = TipoTorneo.Mixto,
                MinEquipos = 8,
                MaxEquipos = 32,
                FechaInicio = DateTime.UtcNow,
                FechaFin = DateTime.UtcNow.AddMonths(1)
            };

            var torneoJson = JsonConvert.SerializeObject(torneo);
            var content = new StringContent(torneoJson, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync("api/Torneos", content).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var torneoCreado = JsonConvert.DeserializeObject<ApiResult<Torneo>>(json);

            // 2. Inscribir 16 equipos
            var equipos = new List<Equipo>();
            for (int i = 1; i <= 16; i++)
            {
                var equipo = new Equipo
                {
                    Nombre = $"Equipo {i}",
                    TorneoId = torneoCreado.Data.Id
                };
                var equipoJson = JsonConvert.SerializeObject(equipo);
                content = new StringContent(equipoJson, Encoding.UTF8, "application/json");
                response = httpClient.PostAsync("api/Equipoes", content).Result;
                json = response.Content.ReadAsStringAsync().Result;
                var equipoCreado = JsonConvert.DeserializeObject<ApiResult<Equipo>>(json);
                equipos.Add(equipoCreado.Data);
            }

            // 3. Iniciar el torneo (generar calendario automático)
            torneoCreado.Data.Estado = EstadoTorneo.Iniciado;
            torneoJson = JsonConvert.SerializeObject(torneoCreado.Data);
            content = new StringContent(torneoJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"api/Torneos/{torneoCreado.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            // 4. Registrar resultados de TODOS los partidos de grupos
            response = httpClient.GetAsync($"api/Partidos?torneoId={torneoCreado.Data.Id}&fase=Grupo").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var partidosGrupo = JsonConvert.DeserializeObject<ApiResult<List<Partido>>>(json);

            var random = new Random();
            foreach (var partido in partidosGrupo.Data)
            {
                partido.GolesLocal = random.Next(0, 5);
                partido.GolesVisitante = random.Next(0, 5);
                partido.IsJugado = true;
                var partidoJson = JsonConvert.SerializeObject(partido);
                content = new StringContent(partidoJson, Encoding.UTF8, "application/json");
                response = httpClient.PutAsync($"api/Partidos/{partido.Id}", content).Result;
                json = response.Content.ReadAsStringAsync().Result;
            }

            // 5. Avanzar a eliminación directa (los 8 clasificados)
            response = httpClient.PostAsync($"api/Torneos/{torneoCreado.Data.Id}/AvanzarRonda", null).Result;
            json = response.Content.ReadAsStringAsync().Result;

            // 6. Registrar resultados de cuartos, semis y final
            var fases = new[] { "Cuartos", "Semifinal", "Final" };
            foreach (var fase in fases)
            {
                response = httpClient.GetAsync($"api/Partidos?torneoId={torneoCreado.Data.Id}&fase={fase}").Result;
                json = response.Content.ReadAsStringAsync().Result;
                var partidosFase = JsonConvert.DeserializeObject<ApiResult<List<Partido>>>(json);

                foreach (var partido in partidosFase.Data)
                {
                    partido.GolesLocal = random.Next(0, 5);
                    partido.GolesVisitante = random.Next(0, 5);
                    partido.IsJugado = true;
                    // Si empate, definir por penales
                    if (partido.GolesLocal == partido.GolesVisitante)
                    {
                        partido.PenalesLocal = random.Next(0, 5);
                        partido.PenalesVisitante = random.Next(0, 5);
                        while (partido.PenalesLocal == partido.PenalesVisitante)
                        {
                            partido.PenalesLocal = random.Next(0, 5);
                            partido.PenalesVisitante = random.Next(0, 5);
                        }
                    }
                    var partidoJson = JsonConvert.SerializeObject(partido);
                    content = new StringContent(partidoJson, Encoding.UTF8, "application/json");
                    response = httpClient.PutAsync($"api/Partidos/{partido.Id}", content).Result;
                    json = response.Content.ReadAsStringAsync().Result;
                }
            }

            // 7. Consultar el campeón
            response = httpClient.GetAsync($"api/Torneos/{torneoCreado.Data.Id}/Campeon").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var campeon = JsonConvert.DeserializeObject<ApiResult<Equipo>>(json);
            if (campeon != null && campeon.Data != null)
                Console.WriteLine($"Campeón: {campeon.Data.Nombre}");
            else
                Console.WriteLine("No se pudo determinar el campeón.");

            // 8. Mostrar la tabla de goleadores
            response = httpClient.GetAsync($"api/EstadisticasJugadores?torneoId={torneoCreado.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var goleadores = JsonConvert.DeserializeObject<ApiResult<List<EstadisticaJugador>>>(json);
            Console.WriteLine("Tabla de goleadores:");
            foreach (var est in goleadores.Data)
            {
                Console.WriteLine($"{est.Jugador?.Nombre}: {est.Goles}");
            }

            // 9. Mostrar historial entre dos equipos específicos
            var equipoA = equipos[0];
            var equipoB = equipos[1];
            response = httpClient.GetAsync($"api/Partidos/Historial?equipoAId={equipoA.Id}&equipoBId={equipoB.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var historial = JsonConvert.DeserializeObject<ApiResult<List<Partido>>>(json);
            Console.WriteLine($"Historial entre {equipoA.Nombre} y {equipoB.Nombre}:");
            if (historial != null && historial.Data != null)
            {
                foreach (var p in historial.Data)
                {
                    Console.WriteLine($"Fecha: {p.Programado}, {p.EquipoLocal?.Nombre} {p.GolesLocal} - {p.GolesVisitante} {p.EquipoVisitante?.Nombre}");
                }
            }
            else
            {
                Console.WriteLine("No se pudo obtener el historial entre los equipos.");
            }

            Console.WriteLine("Prueba de API finalizada.");
            Console.ReadLine();
        }
    }
}
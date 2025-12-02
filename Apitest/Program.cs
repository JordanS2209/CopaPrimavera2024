using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using CopaPrimavera.Modelos;

namespace CopaPrimavera.ApiTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7230/");

            //// TORNEOS
            //TestTorneos(httpClient);

            //// EQUIPOS
            //TestEquipos(httpClient);

            //// JUGADORES
            //TestJugadores(httpClient);

            //// PARTIDOS
            //TestPartidos(httpClient);

            //// GOLES
            //TestGoles(httpClient);

            //// TARJETAS
            //TestTarjetas(httpClient);

            //// GRUPOS
            //TestGrupos(httpClient);

            //// ENCUENTROS LLAVE
            //TestEncuentrosLlave(httpClient);

            //// ESTADISTICAS EQUIPO
            //TestEstadisticasEquipo(httpClient);

            //// ESTADISTICAS JUGADOR
            //TestEstadisticasJugador(httpClient);

            //// SUSPENSIONES
            //TestSuspensiones(httpClient);

            SimularTorneoCompleto(httpClient);

            Console.WriteLine("Pruebas finalizadas. Presiona Enter para salir.");
            Console.ReadLine();
        }

        private static void TestTorneos(HttpClient httpClient)
        {
            var ruta = "api/Torneos";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var torneos = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Torneo>>>(json);

            var nuevo = new Torneo
            {
                Nombre = "Copa Test",
                Tipo = TipoTorneo.Mixto,
                MinEquipos = 8,
                MaxEquipos = 32
            };
            var torneoJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(torneoJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var torneoCreado = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Torneo>>(json);

            torneoCreado.Data.Nombre = "Copa Test Actualizada";
            torneoJson = Newtonsoft.Json.JsonConvert.SerializeObject(torneoCreado.Data);
            content = new StringContent(torneoJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{torneoCreado.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{torneoCreado.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }

        private static void TestEquipos(HttpClient httpClient)
        {
            var ruta = "api/Equipos";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var equipos = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Equipo>>>(json);

            var nuevo = new Equipo
            {
                Nombre = "Equipo Test",
                TorneoId = 1
            };
            var equipoJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(equipoJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var equipoCreado = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Equipo>>(json);

            if (equipoCreado == null || equipoCreado.Data == null)
            {
                Console.WriteLine("Error: El API no devolvió datos válidos al crear el equipo.");
                return;
            }

            equipoCreado.Data.Nombre = "Equipo Test Actualizado";
            equipoJson = Newtonsoft.Json.JsonConvert.SerializeObject(equipoCreado.Data);
            content = new StringContent(equipoJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{equipoCreado.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{equipoCreado.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }

        private static void TestJugadores(HttpClient httpClient)
        {
            var ruta = "api/Jugadores";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var jugadores = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Jugador>>>(json);

            var nuevo = new Jugador
            {
                Nombre = "Jugador Test",
                EquipoId = 1,
                TorneoId = 1
            };
            var jugadorJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(jugadorJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var jugadorCreado = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Jugador>>(json);

            jugadorCreado.Data.Nombre = "Jugador Test Actualizado";
            jugadorJson = Newtonsoft.Json.JsonConvert.SerializeObject(jugadorCreado.Data);
            content = new StringContent(jugadorJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{jugadorCreado.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{jugadorCreado.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }

        private static void TestPartidos(HttpClient httpClient)
        {
            var ruta = "api/Partidos";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var partidos = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Partido>>>(json);

            var nuevo = new Partido
            {
                TorneoId = 1,
                EquipoLocalId = 1,
                EquipoVisitanteId = 2,
                Programado = DateTime.UtcNow.AddDays(1),
                Fase = FasePartido.Grupo
            };
            var partidoJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(partidoJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var partidoCreado = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Partido>>(json);

            partidoCreado.Data.Programado = partidoCreado.Data.Programado.AddHours(1);
            partidoJson = Newtonsoft.Json.JsonConvert.SerializeObject(partidoCreado.Data);
            content = new StringContent(partidoJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{partidoCreado.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{partidoCreado.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }

        private static void TestGoles(HttpClient httpClient)
        {
            var ruta = "api/Goles";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var goles = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Gol>>>(json);

            var nuevo = new Gol
            {
                PartidoId = 1,
                JugadorId = 1,
                Minuto = 10,
                Autogol = false
            };
            var golJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(golJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var golCreado = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Gol>>(json);

            // Validación para evitar NullReferenceException
            if (golCreado == null || golCreado.Data == null)
            {
                Console.WriteLine("Error: El API no devolvió datos válidos al crear el gol.");
                Console.WriteLine($"Respuesta recibida: {json}");
                return;
            }

            golCreado.Data.Minuto += 1;
            golJson = Newtonsoft.Json.JsonConvert.SerializeObject(golCreado.Data);
            content = new StringContent(golJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{golCreado.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{golCreado.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }

        private static void TestTarjetas(HttpClient httpClient)
        {
            var ruta = "api/Tarjetas";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var tarjetas = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Tarjeta>>>(json);

            var nuevo = new Tarjeta
            {
                PartidoId = 1,
                JugadorId = 1,
                Minuto = 30,
                Tipo = "Amarilla"
            };
            var tarjetaJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(tarjetaJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var tarjetaCreada = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Tarjeta>>(json);

            // Validación para evitar NullReferenceException
            if (tarjetaCreada == null || tarjetaCreada.Data == null)
            {
                Console.WriteLine("Error: El API no devolvió datos válidos al crear la tarjeta.");
                Console.WriteLine($"Respuesta recibida: {json}");
                return;
            }

            tarjetaCreada.Data.Minuto += 1;
            tarjetaJson = Newtonsoft.Json.JsonConvert.SerializeObject(tarjetaCreada.Data);
            content = new StringContent(tarjetaJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{tarjetaCreada.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{tarjetaCreada.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }

        private static void TestGrupos(HttpClient httpClient)
        {
            var ruta = "api/Grupos";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var grupos = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Grupo>>>(json);

            var nuevo = new Grupo
            {
                TorneoId = 1,
                Nombre = "Grupo Test"
            };
            var grupoJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(grupoJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var grupoCreado = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Grupo>>(json);

            grupoCreado.Data.Nombre = "Grupo Test Actualizado";
            grupoJson = Newtonsoft.Json.JsonConvert.SerializeObject(grupoCreado.Data);
            content = new StringContent(grupoJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{grupoCreado.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{grupoCreado.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }

        private static void TestEncuentrosLlave(HttpClient httpClient)
        {
            var ruta = "api/EncuentrosLlave";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var encuentros = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<EncuentroLlave>>>(json);

            var nuevo = new EncuentroLlave
            {
                TorneoId = 1
            };
            var encuentroJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(encuentroJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var encuentroCreado = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<EncuentroLlave>>(json);

            if (encuentroCreado == null || encuentroCreado.Data == null)
            {
                Console.WriteLine("Error: El API no devolvió datos válidos al crear el encuentro llave.");
                Console.WriteLine($"Respuesta recibida: {json}");
                return;
            }

            encuentroJson = Newtonsoft.Json.JsonConvert.SerializeObject(encuentroCreado.Data);
            content = new StringContent(encuentroJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{encuentroCreado.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{encuentroCreado.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }

        private static void TestEstadisticasEquipo(HttpClient httpClient)
        {
            var ruta = "api/EstadisticasEquipos";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var estadisticas = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<EstadisticaEquipo>>>(json);

            var nuevo = new EstadisticaEquipo
            {
                EquipoId = 1,
                Puntos = 10,
                Juegos = 5,
                Victorias = 3,
                Empates = 1,
                Derrotas = 1,
                GolesAFavor = 8,
                GolesEnContra = 4,
                TarjetasAmarillas = 2,
                TarjetasRojas = 1
            };
            var estadisticaJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(estadisticaJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var estadisticaCreada = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<EstadisticaEquipo>>(json);

            estadisticaCreada.Data.Puntos += 1;
            estadisticaJson = Newtonsoft.Json.JsonConvert.SerializeObject(estadisticaCreada.Data);
            content = new StringContent(estadisticaJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{estadisticaCreada.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{estadisticaCreada.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }

        private static void TestEstadisticasJugador(HttpClient httpClient)
        {
            var ruta = "api/EstadisticasJugadores";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var estadisticas = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<EstadisticaJugador>>>(json);

            var nuevo = new EstadisticaJugador
            {
                JugadorId = 1,
                Goles = 5,
                TarjetasAmarillas = 1,
                TarjetasRojas = 0
            };
            var estadisticaJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(estadisticaJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var estadisticaCreada = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<EstadisticaJugador>>(json);

            estadisticaCreada.Data.Goles += 1;
            estadisticaJson = Newtonsoft.Json.JsonConvert.SerializeObject(estadisticaCreada.Data);
            content = new StringContent(estadisticaJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{estadisticaCreada.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{estadisticaCreada.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }

        private static void TestSuspensiones(HttpClient httpClient)
        {
            var ruta = "api/Suspensiones";
            var response = httpClient.GetAsync(ruta).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var suspensiones = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Suspension>>>(json);

            var nuevo = new Suspension
            {
                JugadorId = 1,
                Motivo = "Tarjeta Roja",
                CreadoEn = DateTime.UtcNow
            };
            var suspensionJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevo);
            var content = new StringContent(suspensionJson, Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(ruta, content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            var suspensionCreada = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Suspension>>(json);

            suspensionJson = Newtonsoft.Json.JsonConvert.SerializeObject(suspensionCreada.Data);
            content = new StringContent(suspensionJson, Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{ruta}/{suspensionCreada.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            response = httpClient.DeleteAsync($"{ruta}/{suspensionCreada.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
        }
        private static void SimularTorneoCompleto(HttpClient httpClient)
        {
            Console.WriteLine("\n--- FLUJO COMPLETO DE TORNEO ---");

            // 1. Crear torneo tipo "Mixto"
            var torneo = new Torneo
            {
                Nombre = "Copa Primavera 2024",
                Tipo = TipoTorneo.Mixto,
                MinEquipos = 8,
                MaxEquipos = 32
            };
            var torneoJson = Newtonsoft.Json.JsonConvert.SerializeObject(torneo);
            var content = new StringContent(torneoJson, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync("api/Torneos", content).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var torneoCreado = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Torneo>>(json);
            Console.WriteLine($"Torneo creado: {torneoCreado.Data.Nombre} (Id={torneoCreado.Data.Id})");

            // 2. Inscribir 16 equipos
            var equipos = new List<Equipo>();
            for (int i = 1; i <= 16; i++)
            {
                var equipo = new Equipo
                {
                    Nombre = $"Equipo {i:D2}",
                    TorneoId = torneoCreado.Data.Id
                };
                var equipoJson = Newtonsoft.Json.JsonConvert.SerializeObject(equipo);
                content = new StringContent(equipoJson, Encoding.UTF8, "application/json");
                response = httpClient.PostAsync("api/Equipos", content).Result;
                json = response.Content.ReadAsStringAsync().Result;
                var equipoCreado = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Equipo>>(json);
                equipos.Add(equipoCreado.Data);
                Console.WriteLine($"Equipo inscrito: {equipoCreado.Data.Nombre} (Id={equipoCreado.Data.Id})");
            }

            // 3. Iniciar el torneo (generar calendario automático)
            response = httpClient.PostAsync($"api/Torneos/{torneoCreado.Data.Id}/Iniciar", null).Result;
            json = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Torneo iniciado y calendario generado.");

            // 4. Registrar resultados de TODOS los partidos de grupos
            response = httpClient.GetAsync($"api/Partidos?torneoId={torneoCreado.Data.Id}&fase=Grupo").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var partidosGrupo = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Partido>>>(json);
            var rnd = new Random();
            foreach (var partido in partidosGrupo.Data)
            {
                partido.GolesLocal = rnd.Next(0, 5);
                partido.GolesVisitante = rnd.Next(0, 5);
                partido.IsJugado = true;
                var partidoJson = Newtonsoft.Json.JsonConvert.SerializeObject(partido);
                content = new StringContent(partidoJson, Encoding.UTF8, "application/json");
                httpClient.PutAsync($"api/Partidos/{partido.Id}", content).Wait();
                Console.WriteLine($"Grupo: {partido.EquipoLocalId} {partido.GolesLocal} - {partido.GolesVisitante} {partido.EquipoVisitanteId}");
            }

            // 5. Avanzar a eliminación directa (los 8 clasificados)
            response = httpClient.PostAsync($"api/Torneos/{torneoCreado.Data.Id}/AvanzarRonda", null).Result;
            json = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Avanzado a eliminación directa.");

            // 6. Registrar resultados de cuartos, semis y final
            var fases = new[] { "Cuartos", "Semifinal", "Final" };
            foreach (var fase in fases)
            {
                response = httpClient.GetAsync($"api/Partidos?torneoId={torneoCreado.Data.Id}&fase={fase}").Result;
                json = response.Content.ReadAsStringAsync().Result;
                var partidosFase = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Partido>>>(json);
                foreach (var partido in partidosFase.Data)
                {
                    partido.GolesLocal = rnd.Next(0, 5);
                    partido.GolesVisitante = rnd.Next(0, 5);
                    partido.IsJugado = true;
                    if (partido.GolesLocal == partido.GolesVisitante)
                    {
                        partido.PenalesLocal = rnd.Next(0, 5);
                        partido.PenalesVisitante = rnd.Next(0, 5);
                        while (partido.PenalesLocal == partido.PenalesVisitante)
                        {
                            partido.PenalesLocal = rnd.Next(0, 5);
                            partido.PenalesVisitante = rnd.Next(0, 5);
                        }
                    }
                    var partidoJson = Newtonsoft.Json.JsonConvert.SerializeObject(partido);
                    content = new StringContent(partidoJson, Encoding.UTF8, "application/json");
                    httpClient.PutAsync($"api/Partidos/{partido.Id}", content).Wait();
                    Console.WriteLine($"{fase}: {partido.EquipoLocalId} {partido.GolesLocal} - {partido.GolesVisitante} {partido.EquipoVisitanteId}");
                }
            }

            // 7. Consultar el campeón
            response = httpClient.GetAsync($"api/Torneos/{torneoCreado.Data.Id}/Campeon").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var campeon = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<Equipo>>(json);
            if (campeon != null && campeon.Data != null)
                Console.WriteLine($"Campeón: {campeon.Data.Nombre}");
            else
                Console.WriteLine("No se pudo determinar el campeón.");

            // 8. Mostrar la tabla de goleadores
            response = httpClient.GetAsync($"api/EstadisticasJugadores?torneoId={torneoCreado.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var goleadores = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<EstadisticaJugador>>>(json);
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
            var historial = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<List<Partido>>>(json);
            Console.WriteLine($"Historial entre {equipoA.Nombre} y {equipoB.Nombre}:");
            foreach (var p in historial.Data)
            {
                Console.WriteLine($"Fecha: {p.Programado}, {p.EquipoLocalId} {p.GolesLocal} - {p.GolesVisitante} {p.EquipoVisitanteId}");
            }

            Console.WriteLine("--- FLUJO COMPLETO FINALIZADO ---\n");
        }


    }
}
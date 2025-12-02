using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CopaPrimavera.Modelos;

namespace CopaPrimavera.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasJugadoresController : ControllerBase
    {
        private readonly DbbContext _context;

        public EstadisticasJugadoresController(DbbContext context)
        {
            _context = context;
        }

        // GET: api/EstadisticasJugadores
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<EstadisticaJugador>>>> GetEstadisticasJugadores()
        {
            try
            {
                var data = await _context.EstadisticasJugadores
                    .Include(e => e.Jugador)
                    .ToListAsync();
                return ApiResult<List<EstadisticaJugador>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<EstadisticaJugador>>.Fail(ex.Message);
            }
        }

        // GET: api/EstadisticasJugadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<EstadisticaJugador>>> GetEstadisticaJugador(int id)
        {
            try
            {
                var estadistica = await _context.EstadisticasJugadores
                    .Include(e => e.Jugador)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (estadistica == null)
                    return ApiResult<EstadisticaJugador>.Fail("Datos no encontrados");

                return ApiResult<EstadisticaJugador>.Ok(estadistica);
            }
            catch (Exception ex)
            {
                return ApiResult<EstadisticaJugador>.Fail(ex.Message);
            }
        }

        // PUT: api/EstadisticasJugadores/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<EstadisticaJugador>>> PutEstadisticaJugador(int id, EstadisticaJugador estadisticaJugador)
        {
            if (id != estadisticaJugador.Id)
                return ApiResult<EstadisticaJugador>.Fail("No coinciden los identificadores");

            _context.Entry(estadisticaJugador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EstadisticaJugadorExists(id))
                    return ApiResult<EstadisticaJugador>.Fail("Datos no encontrados");
                else
                    return ApiResult<EstadisticaJugador>.Fail(ex.Message);
            }

            return ApiResult<EstadisticaJugador>.Ok(null);
        }

        // POST: api/EstadisticasJugadores
        [HttpPost]
        public async Task<ActionResult<ApiResult<EstadisticaJugador>>> PostEstadisticaJugador(EstadisticaJugador estadisticaJugador)
        {
            try
            {
                _context.EstadisticasJugadores.Add(estadisticaJugador);
                await _context.SaveChangesAsync();

                return ApiResult<EstadisticaJugador>.Ok(estadisticaJugador);
            }
            catch (Exception ex)
            {
                return ApiResult<EstadisticaJugador>.Fail(ex.Message);
            }
        }

        // DELETE: api/EstadisticasJugadores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<EstadisticaJugador>>> DeleteEstadisticaJugador(int id)
        {
            try
            {
                var estadistica = await _context.EstadisticasJugadores.FindAsync(id);
                if (estadistica == null)
                    return ApiResult<EstadisticaJugador>.Fail("Datos no encontrados");

                _context.EstadisticasJugadores.Remove(estadistica);
                await _context.SaveChangesAsync();

                return ApiResult<EstadisticaJugador>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<EstadisticaJugador>.Fail(ex.Message);
            }
        }

        private bool EstadisticaJugadorExists(int id)
        {
            return _context.EstadisticasJugadores.Any(e => e.Id == id);
        }
    }
}
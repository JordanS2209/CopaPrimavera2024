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
    public class JugadoresController : ControllerBase
    {
        private readonly DbbContext _context;

        public JugadoresController(DbbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Jugador>>>> GetJugadores()
        {
            try
            {
                var data = await _context.Jugadores.ToListAsync();
                return ApiResult<List<Jugador>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Jugador>>.Fail(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Jugador>>> GetJugador(int id)
        {
            try
            {
                var jugador = await _context.Jugadores
                    .Include(j => j.Equipo)
                    .FirstOrDefaultAsync(j => j.Id == id);

                if (jugador == null)
                    return ApiResult<Jugador>.Fail("Datos no encontrados");

                return ApiResult<Jugador>.Ok(jugador);
            }
            catch (Exception ex)
            {
                return ApiResult<Jugador>.Fail(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Jugador>>> PutJugador(int id, Jugador jugador)
        {
            if (id != jugador.Id)
                return ApiResult<Jugador>.Fail("No coinciden los identificadores");

            _context.Entry(jugador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!JugadorExists(id))
                    return ApiResult<Jugador>.Fail("Datos no encontrados");
                else
                    return ApiResult<Jugador>.Fail(ex.Message);
            }

            return ApiResult<Jugador>.Ok(null);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<Jugador>>> PostJugador(Jugador jugador)
        {
            try
            {
                _context.Jugadores.Add(jugador);
                await _context.SaveChangesAsync();

                return ApiResult<Jugador>.Ok(jugador);
            }
            catch (Exception ex)
            {
                return ApiResult<Jugador>.Fail(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Jugador>>> DeleteJugador(int id)
        {
            try
            {
                var jugador = await _context.Jugadores.FindAsync(id);
                if (jugador == null)
                    return ApiResult<Jugador>.Fail("Datos no encontrados");

                _context.Jugadores.Remove(jugador);
                await _context.SaveChangesAsync();

                return ApiResult<Jugador>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Jugador>.Fail(ex.Message);
            }
        }

        private bool JugadorExists(int id)
        {
            return _context.Jugadores.Any(e => e.Id == id);
        }
    }
}
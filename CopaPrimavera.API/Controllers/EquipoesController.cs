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
    public class EquipoesController : ControllerBase
    {
        private readonly DbbContext _context;

        public EquipoesController(DbbContext context)
        {
            _context = context;
        }

        // GET: api/Equipoes
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Equipo>>>> GetEquipos()
        {
            try
            {
                var data = await _context.Equipos.ToListAsync();
                return ApiResult<List<Equipo>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Equipo>>.Fail(ex.Message);
            }
        }

        // GET: api/Equipoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Equipo>>> GetEquipo(int id)
        {
            try
            {
                var equipo = await _context.Equipos
                    .Include(e => e.Jugadores)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (equipo == null)
                    return ApiResult<Equipo>.Fail("Datos no encontrados");

                return ApiResult<Equipo>.Ok(equipo);
            }
            catch (Exception ex)
            {
                return ApiResult<Equipo>.Fail(ex.Message);
            }
        }

        // PUT: api/Equipoes/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Equipo>>> PutEquipo(int id, Equipo equipo)
        {
            if (id != equipo.Id)
                return ApiResult<Equipo>.Fail("No coinciden los identificadores");

            _context.Entry(equipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EquipoExists(id))
                    return ApiResult<Equipo>.Fail("Datos no encontrados");
                else
                    return ApiResult<Equipo>.Fail(ex.Message);
            }

            return ApiResult<Equipo>.Ok(null);
        }

        // POST: api/Equipoes
        [HttpPost]
        public async Task<ActionResult<ApiResult<Equipo>>> PostEquipo(Equipo equipo)
        {
            try
            {
                _context.Equipos.Add(equipo);
                await _context.SaveChangesAsync();

                return ApiResult<Equipo>.Ok(equipo);
            }
            catch (Exception ex)
            {
                return ApiResult<Equipo>.Fail(ex.Message);
            }
        }

        // DELETE: api/Equipoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Equipo>>> DeleteEquipo(int id)
        {
            try
            {
                var equipo = await _context.Equipos.FindAsync(id);
                if (equipo == null)
                    return ApiResult<Equipo>.Fail("Datos no encontrados");

                _context.Equipos.Remove(equipo);
                await _context.SaveChangesAsync();

                return ApiResult<Equipo>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Equipo>.Fail(ex.Message);
            }
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
        }
    }
}

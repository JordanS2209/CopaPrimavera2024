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
    public class EstadisticasEquiposController : ControllerBase
    {
        private readonly DbbContext _context;

        public EstadisticasEquiposController(DbbContext context)
        {
            _context = context;
        }

        // GET: api/EstadisticasEquipos
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<EstadisticaEquipo>>>> GetEstadisticasEquipos()
        {
            try
            {
                var data = await _context.EstadisticasEquipos
                    .Include(e => e.Equipo)
                    .ToListAsync();
                return ApiResult<List<EstadisticaEquipo>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<EstadisticaEquipo>>.Fail(ex.Message);
            }
        }

        // GET: api/EstadisticasEquipos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<EstadisticaEquipo>>> GetEstadisticaEquipo(int id)
        {
            try
            {
                var estadistica = await _context.EstadisticasEquipos
                    .Include(e => e.Equipo)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (estadistica == null)
                    return ApiResult<EstadisticaEquipo>.Fail("Datos no encontrados");

                return ApiResult<EstadisticaEquipo>.Ok(estadistica);
            }
            catch (Exception ex)
            {
                return ApiResult<EstadisticaEquipo>.Fail(ex.Message);
            }
        }

        // PUT: api/EstadisticasEquipos/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<EstadisticaEquipo>>> PutEstadisticaEquipo(int id, EstadisticaEquipo estadisticaEquipo)
        {
            if (id != estadisticaEquipo.Id)
                return ApiResult<EstadisticaEquipo>.Fail("No coinciden los identificadores");

            _context.Entry(estadisticaEquipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EstadisticaEquipoExists(id))
                    return ApiResult<EstadisticaEquipo>.Fail("Datos no encontrados");
                else
                    return ApiResult<EstadisticaEquipo>.Fail(ex.Message);
            }

            return ApiResult<EstadisticaEquipo>.Ok(null);
        }

        // POST: api/EstadisticasEquipos
        [HttpPost]
        public async Task<ActionResult<ApiResult<EstadisticaEquipo>>> PostEstadisticaEquipo(EstadisticaEquipo estadisticaEquipo)
        {
            try
            {
                _context.EstadisticasEquipos.Add(estadisticaEquipo);
                await _context.SaveChangesAsync();

                return ApiResult<EstadisticaEquipo>.Ok(estadisticaEquipo);
            }
            catch (Exception ex)
            {
                return ApiResult<EstadisticaEquipo>.Fail(ex.Message);
            }
        }

        // DELETE: api/EstadisticasEquipos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<EstadisticaEquipo>>> DeleteEstadisticaEquipo(int id)
        {
            try
            {
                var estadistica = await _context.EstadisticasEquipos.FindAsync(id);
                if (estadistica == null)
                    return ApiResult<EstadisticaEquipo>.Fail("Datos no encontrados");

                _context.EstadisticasEquipos.Remove(estadistica);
                await _context.SaveChangesAsync();

                return ApiResult<EstadisticaEquipo>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<EstadisticaEquipo>.Fail(ex.Message);
            }
        }

        private bool EstadisticaEquipoExists(int id)
        {
            return _context.EstadisticasEquipos.Any(e => e.Id == id);
        }
    }
}
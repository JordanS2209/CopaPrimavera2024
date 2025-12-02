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
    public class PartidosController : ControllerBase
    {
        private readonly DbbContext _context;

        public PartidosController(DbbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Partido>>>> GetPartidos()
        {
            try
            {
                var data = await _context.Partidos.ToListAsync();
                return ApiResult<List<Partido>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Partido>>.Fail(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Partido>>> GetPartido(int id)
        {
            try
            {
                var partido = await _context.Partidos
                    .Include(p => p.EquipoLocal)
                    .Include(p => p.EquipoVisitante)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (partido == null)
                    return ApiResult<Partido>.Fail("Datos no encontrados");

                return ApiResult<Partido>.Ok(partido);
            }
            catch (Exception ex)
            {
                return ApiResult<Partido>.Fail(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Partido>>> PutPartido(int id, Partido partido)
        {
            if (id != partido.Id)
                return ApiResult<Partido>.Fail("No coinciden los identificadores");

            _context.Entry(partido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PartidoExists(id))
                    return ApiResult<Partido>.Fail("Datos no encontrados");
                else
                    return ApiResult<Partido>.Fail(ex.Message);
            }

            return ApiResult<Partido>.Ok(null);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<Partido>>> PostPartido(Partido partido)
        {
            try
            {
                _context.Partidos.Add(partido);
                await _context.SaveChangesAsync();

                return ApiResult<Partido>.Ok(partido);
            }
            catch (Exception ex)
            {
                return ApiResult<Partido>.Fail(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Partido>>> DeletePartido(int id)
        {
            try
            {
                var partido = await _context.Partidos.FindAsync(id);
                if (partido == null)
                    return ApiResult<Partido>.Fail("Datos no encontrados");

                _context.Partidos.Remove(partido);
                await _context.SaveChangesAsync();

                return ApiResult<Partido>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Partido>.Fail(ex.Message);
            }
        }

        private bool PartidoExists(int id)
        {
            return _context.Partidos.Any(e => e.Id == id);
        }
    }
}
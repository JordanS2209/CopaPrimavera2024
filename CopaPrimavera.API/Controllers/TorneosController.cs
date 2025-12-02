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
    public class TorneosController : ControllerBase
    {
        private readonly DbbContext _context;

        public TorneosController(DbbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Torneo>>>> GetTorneos()
        {
            try
            {
                var data = await _context.Torneos.ToListAsync();
                return ApiResult<List<Torneo>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Torneo>>.Fail(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Torneo>>> GetTorneo(int id)
        {
            try
            {
                var torneo = await _context.Torneos
                    .Include(t => t.Equipos)
                    .Include(t => t.Partidos)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (torneo == null)
                    return ApiResult<Torneo>.Fail("Datos no encontrados");

                return ApiResult<Torneo>.Ok(torneo);
            }
            catch (Exception ex)
            {
                return ApiResult<Torneo>.Fail(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Torneo>>> PutTorneo(int id, Torneo torneo)
        {
            if (id != torneo.Id)
                return ApiResult<Torneo>.Fail("No coinciden los identificadores");

            _context.Entry(torneo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TorneoExists(id))
                    return ApiResult<Torneo>.Fail("Datos no encontrados");
                else
                    return ApiResult<Torneo>.Fail(ex.Message);
            }

            return ApiResult<Torneo>.Ok(null);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<Torneo>>> PostTorneo(Torneo torneo)
        {
            try
            {
                _context.Torneos.Add(torneo);
                await _context.SaveChangesAsync();

                return ApiResult<Torneo>.Ok(torneo);
            }
            catch (Exception ex)
            {
                return ApiResult<Torneo>.Fail(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Torneo>>> DeleteTorneo(int id)
        {
            try
            {
                var torneo = await _context.Torneos.FindAsync(id);
                if (torneo == null)
                    return ApiResult<Torneo>.Fail("Datos no encontrados");

                _context.Torneos.Remove(torneo);
                await _context.SaveChangesAsync();

                return ApiResult<Torneo>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Torneo>.Fail(ex.Message);
            }
        }

        private bool TorneoExists(int id)
        {
            return _context.Torneos.Any(e => e.Id == id);
        }
    }
}
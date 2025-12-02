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
    public class GolesController : ControllerBase
    {
        private readonly DbbContext _context;

        public GolesController(DbbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Gol>>>> GetGoles()
        {
            try
            {
                var data = await _context.Goles.ToListAsync();
                return ApiResult<List<Gol>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Gol>>.Fail(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Gol>>> GetGol(int id)
        {
            try
            {
                var gol = await _context.Goles
                    .Include(g => g.Jugador)
                    .Include(g => g.Partido)
                    .FirstOrDefaultAsync(g => g.Id == id);

                if (gol == null)
                    return ApiResult<Gol>.Fail("Datos no encontrados");

                return ApiResult<Gol>.Ok(gol);
            }
            catch (Exception ex)
            {
                return ApiResult<Gol>.Fail(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Gol>>> PutGol(int id, Gol gol)
        {
            if (id != gol.Id)
                return ApiResult<Gol>.Fail("No coinciden los identificadores");

            _context.Entry(gol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!GolExists(id))
                    return ApiResult<Gol>.Fail("Datos no encontrados");
                else
                    return ApiResult<Gol>.Fail(ex.Message);
            }

            return ApiResult<Gol>.Ok(null);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<Gol>>> PostGol(Gol gol)
        {
            try
            {
                _context.Goles.Add(gol);
                await _context.SaveChangesAsync();

                return ApiResult<Gol>.Ok(gol);
            }
            catch (Exception ex)
            {
                return ApiResult<Gol>.Fail(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Gol>>> DeleteGol(int id)
        {
            try
            {
                var gol = await _context.Goles.FindAsync(id);
                if (gol == null)
                    return ApiResult<Gol>.Fail("Datos no encontrados");

                _context.Goles.Remove(gol);
                await _context.SaveChangesAsync();

                return ApiResult<Gol>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Gol>.Fail(ex.Message);
            }
        }

        private bool GolExists(int id)
        {
            return _context.Goles.Any(e => e.Id == id);
        }
    }
}
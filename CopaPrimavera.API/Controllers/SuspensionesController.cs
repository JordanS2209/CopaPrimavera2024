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
    public class SuspensionesController : ControllerBase
    {
        private readonly DbbContext _context;

        public SuspensionesController(DbbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Suspension>>>> GetSuspensiones()
        {
            try
            {
                var data = await _context.Suspensiones.ToListAsync();
                return ApiResult<List<Suspension>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Suspension>>.Fail(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Suspension>>> GetSuspension(int id)
        {
            try
            {
                var suspension = await _context.Suspensiones
                    .Include(s => s.Jugador)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (suspension == null)
                    return ApiResult<Suspension>.Fail("Datos no encontrados");

                return ApiResult<Suspension>.Ok(suspension);
            }
            catch (Exception ex)
            {
                return ApiResult<Suspension>.Fail(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Suspension>>> PutSuspension(int id, Suspension suspension)
        {
            if (id != suspension.Id)
                return ApiResult<Suspension>.Fail("No coinciden los identificadores");

            _context.Entry(suspension).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!SuspensionExists(id))
                    return ApiResult<Suspension>.Fail("Datos no encontrados");
                else
                    return ApiResult<Suspension>.Fail(ex.Message);
            }

            return ApiResult<Suspension>.Ok(null);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<Suspension>>> PostSuspension(Suspension suspension)
        {
            try
            {
                _context.Suspensiones.Add(suspension);
                await _context.SaveChangesAsync();

                return ApiResult<Suspension>.Ok(suspension);
            }
            catch (Exception ex)
            {
                return ApiResult<Suspension>.Fail(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Suspension>>> DeleteSuspension(int id)
        {
            try
            {
                var suspension = await _context.Suspensiones.FindAsync(id);
                if (suspension == null)
                    return ApiResult<Suspension>.Fail("Datos no encontrados");

                _context.Suspensiones.Remove(suspension);
                await _context.SaveChangesAsync();

                return ApiResult<Suspension>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Suspension>.Fail(ex.Message);
            }
        }

        private bool SuspensionExists(int id)
        {
            return _context.Suspensiones.Any(e => e.Id == id);
        }
    }
}
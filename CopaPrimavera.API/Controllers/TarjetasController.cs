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
    public class TarjetasController : ControllerBase
    {
        private readonly DbbContext _context;

        public TarjetasController(DbbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Tarjeta>>>> GetTarjetas()
        {
            try
            {
                var data = await _context.Tarjetas.ToListAsync();
                return ApiResult<List<Tarjeta>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Tarjeta>>.Fail(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Tarjeta>>> GetTarjeta(int id)
        {
            try
            {
                var tarjeta = await _context.Tarjetas
                    .Include(t => t.Jugador)
                    .Include(t => t.Partido)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (tarjeta == null)
                    return ApiResult<Tarjeta>.Fail("Datos no encontrados");

                return ApiResult<Tarjeta>.Ok(tarjeta);
            }
            catch (Exception ex)
            {
                return ApiResult<Tarjeta>.Fail(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Tarjeta>>> PutTarjeta(int id, Tarjeta tarjeta)
        {
            if (id != tarjeta.Id)
                return ApiResult<Tarjeta>.Fail("No coinciden los identificadores");

            _context.Entry(tarjeta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TarjetaExists(id))
                    return ApiResult<Tarjeta>.Fail("Datos no encontrados");
                else
                    return ApiResult<Tarjeta>.Fail(ex.Message);
            }

            return ApiResult<Tarjeta>.Ok(null);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<Tarjeta>>> PostTarjeta(Tarjeta tarjeta)
        {
            try
            {
                _context.Tarjetas.Add(tarjeta);
                await _context.SaveChangesAsync();

                return ApiResult<Tarjeta>.Ok(tarjeta);
            }
            catch (Exception ex)
            {
                return ApiResult<Tarjeta>.Fail(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Tarjeta>>> DeleteTarjeta(int id)
        {
            try
            {
                var tarjeta = await _context.Tarjetas.FindAsync(id);
                if (tarjeta == null)
                    return ApiResult<Tarjeta>.Fail("Datos no encontrados");

                _context.Tarjetas.Remove(tarjeta);
                await _context.SaveChangesAsync();

                return ApiResult<Tarjeta>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Tarjeta>.Fail(ex.Message);
            }
        }

        private bool TarjetaExists(int id)
        {
            return _context.Tarjetas.Any(e => e.Id == id);
        }
    }
}
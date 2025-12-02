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
    public class EncuentrosLlavesController : ControllerBase
    {
        private readonly DbbContext _context;

        public EncuentrosLlavesController(DbbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<EncuentroLlave>>>> GetEncuentrosLlave()
        {
            try
            {
                var data = await _context.EncuentrosLlaves.ToListAsync();
                return ApiResult<List<EncuentroLlave>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<EncuentroLlave>>.Fail(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<EncuentroLlave>>> GetEncuentroLlave(int id)
        {
            try
            {
                var encuentro = await _context.EncuentrosLlaves
                    .Include(e => e.Torneo)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (encuentro == null)
                    return ApiResult<EncuentroLlave>.Fail("Datos no encontrados");

                return ApiResult<EncuentroLlave>.Ok(encuentro);
            }
            catch (Exception ex)
            {
                return ApiResult<EncuentroLlave>.Fail(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<EncuentroLlave>>> PutEncuentroLlave(int id, EncuentroLlave encuentro)
        {
            if (id != encuentro.Id)
                return ApiResult<EncuentroLlave>.Fail("No coinciden los identificadores");

            _context.Entry(encuentro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EncuentroLlaveExists(id))
                    return ApiResult<EncuentroLlave>.Fail("Datos no encontrados");
                else
                    return ApiResult<EncuentroLlave>.Fail(ex.Message);
            }

            return ApiResult<EncuentroLlave>.Ok(null);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<EncuentroLlave>>> PostEncuentroLlave(EncuentroLlave encuentro)
        {
            try
            {
                _context.EncuentrosLlaves.Add(encuentro);
                await _context.SaveChangesAsync();

                return ApiResult<EncuentroLlave>.Ok(encuentro);
            }
            catch (Exception ex)
            {
                return ApiResult<EncuentroLlave>.Fail(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<EncuentroLlave>>> DeleteEncuentroLlave(int id)
        {
            try
            {
                var encuentro = await _context.EncuentrosLlaves.FindAsync(id);
                if (encuentro == null)
                    return ApiResult<EncuentroLlave>.Fail("Datos no encontrados");

                _context.EncuentrosLlaves.Remove(encuentro);
                await _context.SaveChangesAsync();

                return ApiResult<EncuentroLlave>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<EncuentroLlave>.Fail(ex.Message);
            }
        }

        private bool EncuentroLlaveExists(int id)
        {
            return _context.EncuentrosLlaves.Any(e => e.Id == id);
        }
    }
}
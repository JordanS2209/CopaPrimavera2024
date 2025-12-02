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
    public class GruposController : ControllerBase
    {
        private readonly DbbContext _context;

        public GruposController(DbbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Grupo>>>> GetGrupos()
        {
            try
            {
                var data = await _context.Grupos.ToListAsync();
                return ApiResult<List<Grupo>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Grupo>>.Fail(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Grupo>>> GetGrupo(int id)
        {
            try
            {
                var grupo = await _context.Grupos.FirstOrDefaultAsync(g => g.Id == id);

                if (grupo == null)
                    return ApiResult<Grupo>.Fail("Datos no encontrados");

                return ApiResult<Grupo>.Ok(grupo);
            }
            catch (Exception ex)
            {
                return ApiResult<Grupo>.Fail(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Grupo>>> PutGrupo(int id, Grupo grupo)
        {
            if (id != grupo.Id)
                return ApiResult<Grupo>.Fail("No coinciden los identificadores");

            _context.Entry(grupo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!GrupoExists(id))
                    return ApiResult<Grupo>.Fail("Datos no encontrados");
                else
                    return ApiResult<Grupo>.Fail(ex.Message);
            }

            return ApiResult<Grupo>.Ok(null);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<Grupo>>> PostGrupo(Grupo grupo)
        {
            try
            {
                _context.Grupos.Add(grupo);
                await _context.SaveChangesAsync();

                return ApiResult<Grupo>.Ok(grupo);
            }
            catch (Exception ex)
            {
                return ApiResult<Grupo>.Fail(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Grupo>>> DeleteGrupo(int id)
        {
            try
            {
                var grupo = await _context.Grupos.FindAsync(id);
                if (grupo == null)
                    return ApiResult<Grupo>.Fail("Datos no encontrados");

                _context.Grupos.Remove(grupo);
                await _context.SaveChangesAsync();

                return ApiResult<Grupo>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Grupo>.Fail(ex.Message);
            }
        }

        private bool GrupoExists(int id)
        {
            return _context.Grupos.Any(e => e.Id == id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiInstrumentos.Data;

namespace ApiInstrumentos.Controllers
{
    [Route("api/instrumentos")]
    [ApiController]
    public class InstrumentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InstrumentosController(AppDbContext context)
        {
            _context = context;
        }

        // ex -> GET: api/Instrumentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrumento>>> GetInstrumentos()
        {
            return await _context.Instrumentos.ToListAsync();
        }

        // GET: api/Instrumentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instrumento>> GetInstrumento(int id)
        {
            var instrumento = await _context.Instrumentos.FindAsync(id);

            if (instrumento == null)
            {
                return NotFound();
            }

            return instrumento;
        }

        // ex -> PUT: api/Instrumentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstrumento(int id, Instrumento instrumento)
        {
            if (id != instrumento.id)
            {
                return BadRequest();
            }

            _context.Entry(instrumento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrumentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // ex -> POST: api/Instrumentos
        [HttpPost]
        public async Task<ActionResult<Instrumento>> PostInstrumento(Instrumento instrumento)
        {
            _context.Instrumentos.Add(instrumento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstrumento", new { id = instrumento.id }, instrumento);
        }

        // ex -> DELETE: api/Instrumentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstrumento(int id)
        {
            var instrumento = await _context.Instrumentos.FindAsync(id);
            if (instrumento == null)
            {
                return NotFound();
            }

            _context.Instrumentos.Remove(instrumento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstrumentoExists(int id)
        {
            return _context.Instrumentos.Any(e => e.id == id);
        }
    }
}

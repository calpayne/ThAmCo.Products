using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Products.Data;

namespace ThAmCo.Products.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly StoreDb _context;

        public TypesController(StoreDb context)
        {
            _context = context;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PType>>> GetTypes()
        {
            return await _context.Types.ToListAsync();
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PType>> GetPType(int id)
        {
            var pType = await _context.Types.FindAsync(id);

            if (pType == null)
            {
                return NotFound();
            }

            return pType;
        }

        // PUT: api/Types/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPType(int id, PType pType)
        {
            if (id != pType.Id)
            {
                return BadRequest();
            }

            _context.Entry(pType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PTypeExists(id))
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

        // POST: api/Types
        [HttpPost]
        public async Task<ActionResult<PType>> PostPType(PType pType)
        {
            _context.Types.Add(pType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPType", new { id = pType.Id }, pType);
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PType>> DeletePType(int id)
        {
            var pType = await _context.Types.FindAsync(id);
            if (pType == null)
            {
                return NotFound();
            }

            _context.Types.Remove(pType);
            await _context.SaveChangesAsync();

            return pType;
        }

        private bool PTypeExists(int id)
        {
            return _context.Types.Any(e => e.Id == id);
        }
    }
}

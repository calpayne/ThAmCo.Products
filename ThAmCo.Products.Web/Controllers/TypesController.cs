using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Products.Data;
using ThAmCo.Products.Models;

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
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            return await _context.Types.Select(t => TypeDto.Transform(t)).ToListAsync();
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeDto>> GetType(int id)
        {
            var type = await _context.Types.Select(t => TypeDto.Transform(t)).FirstOrDefaultAsync(t => t.Id == id);

            if (type == null)
            {
                return NotFound();
            }

            return type;
        }

        // PUT: api/Types/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutType(int id, TypeDto type)
        {
            if (id != type.Id)
            {
                return BadRequest();
            }

            _context.Entry(TypeDto.ToType(type)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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
        public async Task<ActionResult<TypeDto>> PostType(TypeDto type)
        {
            _context.Types.Add(TypeDto.ToType(type));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetType", new { id = type.Id }, TypeDto.ToType(type));
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeDto>> DeleteType(int id)
        {
            var type = await _context.Types.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            _context.Types.Remove(type);
            await _context.SaveChangesAsync();

            return TypeDto.Transform(type);
        }

        private bool TypeExists(int id)
        {
            return _context.Types.Any(e => e.Id == id);
        }
    }
}

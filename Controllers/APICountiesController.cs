using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeDatabase.Data;
using EmployeeDatabase.Models;

namespace EmployeeDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APICountiesController : ControllerBase
    {
        private readonly EmployeeDatabaseContext _context;

        public APICountiesController(EmployeeDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/APICounties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<County>>> GetCounty()
        {
            return await _context.County.ToListAsync();
        }

        // GET: api/APICounties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<County>> GetCounty(int id)
        {
            var county = await _context.County
              .Where(b => b.Id == id)
              .Include(b => b.SubCounties)
              .FirstOrDefaultAsync(m => m.Id == id);

            if (county == null)
            {
                return NotFound();
            }

            return county;
        }

        // PUT: api/APICounties/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCounty(int id, County county)
        {
            if (id != county.Id)
            {
                return BadRequest();
            }

            _context.Entry(county).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountyExists(id))
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

        // POST: api/APICounties
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<County>> PostCounty(County county)
        {
            _context.County.Add(county);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCounty", new { id = county.Id }, county);
        }

        // DELETE: api/APICounties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<County>> DeleteCounty(int id)
        {
            var county = await _context.County.FindAsync(id);
            if (county == null)
            {
                return NotFound();
            }

            _context.County.Remove(county);
            await _context.SaveChangesAsync();

            return county;
        }

        private bool CountyExists(int id)
        {
            return _context.County.Any(e => e.Id == id);
        }
    }
}

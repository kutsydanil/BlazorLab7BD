using CinemaCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace WebApplicationLab6.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly CinemaContext _context;

        public FilmsController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Films>>> Get()
        {
            var result = await _context.Films.Include(f => f.FilmProduction).Include(c => c.CountryProduction).Include(g => g.Genre).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int? id)
        {
            Films film = await _context.Films.Include(f => f.FilmProduction).Include(c => c.CountryProduction).Include(g => g.Genre).FirstOrDefaultAsync(f => f.Id == id);
            if(film != null)
            {
                return Ok(film);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Films film)
        {
            if (ModelState.IsValid)
            {
                if (film != null)
                {
                    _context.Films.Add(film);
                    await _context.SaveChangesAsync();
                    return Ok(film);
                }
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Films film)
        {
            if (ModelState.IsValid)
            {
                if (film != null)
                {
                    if (await _context.Films.AnyAsync(f => f.Id == film.Id))
                    {
                        _context.Update(film);
                        await _context.SaveChangesAsync();
                        return Ok(film);
                    }
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id) {
            Films film = await _context.Films.FirstOrDefaultAsync(f => f.Id == id);
            if(film != null)
            {
                _context.Films.Remove(film);
                await _context.SaveChangesAsync();
                return Ok(film);
            }
            return NotFound();
        }
    }
}

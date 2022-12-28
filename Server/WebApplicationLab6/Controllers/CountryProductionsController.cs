using CinemaCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationLab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryProductionsController : Controller
    {
        private readonly CinemaContext _context;

        public CountryProductionsController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<CountryProductions>> GetList()
        {
            var canAccept = CanAccept((User)HttpContext.Items["User"]) == false;
            if (canAccept == false)
            {
                return Unauthorized();
            }
            return Ok(_context.CountryProductions.ToList());
        }

        [HttpGet("{id}/")]
        public ActionResult<CountryProductions> Get(int id)
        {
            var countryProd = _context.CountryProductions.Where(item => item.Id == id).FirstOrDefault();

            if(countryProd == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(countryProd);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CountryProductions countryProductions)
        {
            if (ModelState.IsValid)
            {
                if (countryProductions != null)
                {
                    _context.CountryProductions.Add(countryProductions);
                    await _context.SaveChangesAsync();
                    return Ok(countryProductions);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(CountryProductions countryProductions)
        {
            if (ModelState.IsValid)
            {
                if(countryProductions != null)
                {
                    var item = _context.CountryProductions.Where(item => item.Id == countryProductions.Id).FirstOrDefault();
                    if(item != null)
                    {
                        item.Name = countryProductions.Name;
                        await _context.SaveChangesAsync();
                        return Ok(countryProductions);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CountryProductions countryProductions = _context.CountryProductions.Where(f => f.Id == id).FirstOrDefault();
            if (countryProductions != null)
            {
                _context.CountryProductions.Remove(countryProductions);
                await _context.SaveChangesAsync();
                return Ok(countryProductions);
            }
            return NotFound();
        }

        private bool CanAccept(User user)
        {
            return user == null;
        }
    }
}

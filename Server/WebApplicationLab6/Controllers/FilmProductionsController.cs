using CinemaCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationLab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmProductionsController : Controller
    {
        private readonly CinemaContext _context;

        public FilmProductionsController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<FilmProductions> Get()
        {
            return _context.FilmProductions.AsEnumerable();
        }
    }
}

using CinemaCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationLab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        private readonly CinemaContext _context;

        public GenresController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Genres> Get()
        {
            return _context.Genres.AsEnumerable();
        }
    }
}

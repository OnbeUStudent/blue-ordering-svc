using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dii_OrderingSvc.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Dii_OrderingSvc.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly OrderingSvcContext _context;

        public MoviesController(OrderingSvcContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies
                .Include(movie => movie.MovieMetadata)
                .ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(string id)
        {
            if (!Guid.TryParse(id, out Guid movieIdAsGuid))
            {
                return NotFound();
            }
            var movie = await _context.Movies
                .Include(movie => movie.MovieMetadata)
                .SingleOrDefaultAsync(movie => movie.MovieId == movieIdAsGuid);
            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        private bool MovieExists(Guid id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}

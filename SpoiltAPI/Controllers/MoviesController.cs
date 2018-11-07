using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpoiltAPI.Data;
using SpoiltAPI.Models;
using SpoiltAPI.Models.Interfaces;
using SpoiltAPI.Models.ViewModels;

namespace SpoiltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly SpoiltContext _context;
        private readonly IMovie _movieContext;

        public MoviesController(SpoiltContext context, IMovie movieContext)
        {
            _context = context;
            _movieContext = movieContext;
        }

        // GET: api/Movies
        [HttpGet]
        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies;
        }

        // GET: api/Movies/5
        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetMovie([FromRoute] string imdbId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await _context.Movies
                //.Include(c => c.Spoilers)
                
                .FirstOrDefaultAsync(m => m.ID == imdbId);

            // If movie isn't in our database, try to get it from external api
            if (movie == null)
            {
                MovieDescription mDescripton = await _movieContext.GetMovieExternal(imdbId);

                if(mDescripton == null) { 
                    return NotFound();
                }

                 movie = new Movie {ID = mDescripton.ImdbID, Title = mDescripton.Title, Genre = mDescripton.Genre, Plot = mDescripton.Plot, Year = int.Parse(mDescripton.Year), Poster = mDescripton.Poster};
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();

                return Ok(movie);
            }

          
            movie.Spoilers = await _context.Spoilers.Where(x => x.MovieID == movie.ID).ToListAsync();


            return Ok(movie);
        }

        [HttpGet("search")]
        public async Task<OMDBSearchResponse> SearchMovies(string term)
        {
            return await _movieContext.SearchMovie(term);
        }

        // PUT: api/Movies/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMovie([FromRoute] string id, [FromBody] Movie movie)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != movie.IMDBID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(movie).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Movies
        //[HttpPost]
        //public async Task<IActionResult> PostMovie([FromBody] Movie movie)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Movies.Add(movie);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMovie", new { id = movie.ID }, movie);
        //}

        //// DELETE: api/Movies/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var movie = await _context.Movies.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Movies.Remove(movie);
        //    await _context.SaveChangesAsync();

        //    return Ok(movie);
        //}

        private bool MovieExists(string id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }
    }
}
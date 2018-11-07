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
        /// <summary>
        /// Gets the movies.
        /// </summary>
        /// <returns>Returns Movies</returns>
        [HttpGet]
        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies;
        }

        // GET: api/Movies/5        
        /// <summary>
        /// Gets the movie.
        /// </summary>
        /// <param name="imdbId">The imdb identifier.</param>
        /// <returns>Returns OK</returns>
        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetMovie([FromRoute] string imdbId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await _movieContext.GetMovieOrCreate(imdbId, true);
            return Ok(movie);
        }

        /// <summary>
        /// Searches the movies.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns>Returns Search Response from OMDB API</returns>
        [HttpGet("search")]
        public async Task<OMDBSearchResponse> SearchMovies(string term)
        {
            return await _movieContext.SearchMovie(term);
        }

        /// <summary>
        /// Movies the exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns true if Movie exists</returns>
        private bool MovieExists(string id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }
    }
}
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
        /// <summary>
        /// The context
        /// </summary>
        private readonly SpoiltContext _context;

        /// <summary>
        /// The movie context
        /// </summary>
        private readonly IMovie _movieContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="movieContext">The movie context.</param>
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
        public IEnumerable<Movie> GetMovies(int pageNumber = 1, int pageSize = 15)
        {
            return _movieContext.RetrieveMovies(pageNumber, pageSize);
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
        [ResponseCache(Duration = 3000, VaryByQueryKeys = new string[] { "term" })]
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
            return _movieContext.CheckMovieExists(id);
        }
    }
}
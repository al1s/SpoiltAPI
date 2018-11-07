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

namespace SpoiltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpoilersController : ControllerBase
    {
        private readonly SpoiltContext _context;
        private readonly IMovie _movieContext;

        public SpoilersController(SpoiltContext context, IMovie movieContext)
        {
            _context = context;
            _movieContext = movieContext;
        }

        // GET: api/Spoilers
        /// <summary>
        /// Gets Spoilers
        /// </summary>
        /// <returns>Returns all Spoilers</returns>
        [HttpGet]
        public IEnumerable<Spoiler> GetSpoilers()
        {
            return _context.Spoilers.Include(s => s.Movie);
        }

        // GET: api/Spoilers/5
        /// <summary>
        /// Gets a Spoiler
        /// </summary>
        /// <param name="id">ID of a Spoiler</param>
        /// <returns>Returns a specific Spoiler</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpoiler([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spoiler = await _context.Spoilers.Include(s => s.Movie).FirstOrDefaultAsync(s => s.ID == id);

            if (spoiler == null)
            {
                return NotFound();
            }

            return Ok(spoiler);
        }

        // PUT: api/Spoilers/5
        /// <summary>
        /// Updates a Spoiler
        /// </summary>
        /// <param name="id">ID of a Spoiler</param>
        /// <param name="spoiler">Spoiler information to be Updated</param>
        /// <returns>Returns NoContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpoiler([FromRoute] int id, [FromBody] Spoiler spoiler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spoiler.ID)
            {
                return BadRequest();
            }

            _context.Entry(spoiler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpoilerExists(id))
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

        // POST: api/Spoilers
        /// <summary>
        /// Create a Spoiler
        /// </summary>
        /// <param name="spoiler">Spoiler information</param>
        /// <returns>Returns the Spoiler that was Created</returns>
        [HttpPost]
        public async Task<IActionResult> PostSpoiler([FromBody] Spoiler spoiler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await _movieContext.GetMovieOrCreate(spoiler.MovieID, false);
            if (movie == null)
            {
                return BadRequest("Movie does not exist.");
            }

            _context.Spoilers.Add(spoiler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpoiler", new { id = spoiler.ID }, spoiler);
        }

        // DELETE: api/Spoilers/5
        /// <summary>
        /// Delete a Spoiler
        /// </summary>
        /// <param name="id">ID of a Spoiler</param>
        /// <returns>Returns OK</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpoiler([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spoiler = await _context.Spoilers.FindAsync(id);
            if (spoiler == null)
            {
                return NotFound();
            }

            _context.Spoilers.Remove(spoiler);
            await _context.SaveChangesAsync();

            return Ok(spoiler);
        }

        /// <summary>
        /// Checks if a Spoiler exists
        /// </summary>
        /// <param name="id">ID of a Spoiler</param>
        /// <returns>Returns true if a Spoiler exists and false if a Spoiler does not exist</returns>
        private bool SpoilerExists(int id)
        {
            return _context.Spoilers.Any(e => e.ID == id);
        }
    }
}
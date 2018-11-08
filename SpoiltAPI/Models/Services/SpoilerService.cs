using Microsoft.EntityFrameworkCore;
using SpoiltAPI.Data;
using SpoiltAPI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpoiltAPI.Models.Services
{
    public class SpoilerService : ISpoiler
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly SpoiltContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpoilerService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SpoilerService(SpoiltContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Removes the spoiler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async void RemoveSpoiler(int id)
        {
            var spoiler = await _context.Spoilers.FindAsync(id);

            _context.Spoilers.Remove(spoiler);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves the spoiler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns a Spoiler
        /// </returns>
        public async Task<Spoiler> RetrieveSpoiler(int id)
        {
            return await _context.Spoilers.Include(s => s.Movie).FirstOrDefaultAsync(s => s.ID == id);
        }

        /// <summary>
        /// Retrieves the spoilers.
        /// </summary>
        /// <returns>
        /// Returns a list of Spoilers
        /// </returns>
        public IEnumerable<Spoiler> RetrieveSpoilers()
        {
            return _context.Spoilers.Include(s => s.Movie);
        }

        /// <summary>
        /// Creates the spoiler.
        /// </summary>
        /// <param name="spoiler">The spoiler.</param>
        /// <returns>
        /// Returns Spoiler that was Created
        /// </returns>
        public async Task<Spoiler> CreateSpoiler(Spoiler spoiler)
        {
            _context.Spoilers.Add(spoiler);
            await _context.SaveChangesAsync();

            return await RetrieveSpoiler(spoiler.ID);
        }

        /// <summary>
        /// Updates the spoiler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="spoiler">The spoiler.</param>
        /// <returns>
        /// Returns the Spoiler that was Updated
        /// </returns>
        public Task<Spoiler> UpdateSpoiler(int id, Spoiler spoiler)
        {
            _context.Entry(spoiler).State = EntityState.Modified;

            return RetrieveSpoiler(id);
        }

        /// <summary>
        /// Checks the spoiler exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns true if Spoiler exists and false is Spoiler does not exist
        /// </returns>
        public bool CheckSpoilerExists(int id)
        {
            return _context.Spoilers.Any(e => e.ID == id);
        }
    }
}

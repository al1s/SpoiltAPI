using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpoiltAPI.Models.Interfaces
{
    public interface ISpoiler
    {
        /// <summary>
        /// Checks the spoiler exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns true if Spoiler exists and false is Spoiler does not exist</returns>
        bool CheckSpoilerExists(int id);

        /// <summary>
        /// Creates the spoiler.
        /// </summary>
        /// <param name="spoiler">The spoiler.</param>
        /// <returns>Returns Spoiler that was Created</returns>
        Task<Spoiler> CreateSpoiler(Spoiler spoiler);

        /// <summary>
        /// Removes the spoiler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void RemoveSpoiler(int id);

        /// <summary>
        /// Retrieves the spoiler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns a Spoiler</returns>
        Task<Spoiler> RetrieveSpoiler(int id);

        /// <summary>
        /// Retrieves the spoilers.
        /// </summary>
        /// <returns>Returns a list of Spoilers</returns>
        IEnumerable<Spoiler> RetrieveSpoilers();

        /// <summary>
        /// Updates the spoiler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="spoiler">The spoiler.</param>
        /// <returns>Returns the Spoiler that was Updated</returns>
        Task<Spoiler> UpdateSpoiler(int id, Spoiler spoiler);
    }
}

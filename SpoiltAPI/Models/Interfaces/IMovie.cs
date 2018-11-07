using SpoiltAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpoiltAPI.Models.Interfaces
{
    public interface IMovie
    {
        /// <summary>
        /// Searches the movie.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        Task<OMDBSearchResponse> SearchMovie(string term);

        /// <summary>
        /// Gets the movie external.
        /// </summary>
        /// <param name="imdbId">The imdb identifier.</param>
        /// <returns></returns>
        Task<MovieDescription> GetMovieExternal(string imdbId);

        /// <summary>
        /// Gets the movie.
        /// </summary>
        /// <param name="imdbId">The imdb identifier.</param>
        /// <returns></returns>
        Task<Movie> GetMovieOrCreate(string imdbId, bool loadSpoilers = false);

    }
}

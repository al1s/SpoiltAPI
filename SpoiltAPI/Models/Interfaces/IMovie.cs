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
        /// <returns>Returns an OMDB Search Response</returns>
        Task<OMDBSearchResponse> SearchMovie(string term);

        /// <summary>
        /// Retrieves the movies.
        /// </summary>
        /// <returns>Returns a list of Movies</returns>
        IEnumerable<Movie> RetrieveMovies();

        /// <summary>
        /// Gets the movie external.
        /// </summary>
        /// <param name="imdbId">The imdb identifier.</param>
        /// <returns>Returns a Movie Description</returns>
        Task<MovieDescription> GetMovieExternal(string imdbId);

        /// <summary>
        /// Gets the movie.
        /// </summary>
        /// <param name="imdbId">The imdb identifier.</param>
        /// <returns>Returns a Movie</returns>
        Task<Movie> GetMovieOrCreate(string imdbId, bool loadSpoilers = false);

        bool CheckMovieExists(string id);
    }
}

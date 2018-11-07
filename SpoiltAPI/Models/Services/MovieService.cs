using SpoiltAPI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using SpoiltAPI.Models.ViewModels;
using SpoiltAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SpoiltAPI.Models.Services
{
    public class MovieService : IMovie
    {
        private readonly SpoiltContext _context;

        public MovieService(SpoiltContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Searches for a Movie on the OMDB API
        /// </summary>
        /// <param name="term">User search parameters</param>
        /// <returns>Returns OMDB Search Results</returns>
        public async Task<OMDBSearchResponse> SearchMovie(string term)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://www.omdbapi.com/");
                    var response = await client.GetAsync($"?s={term}&apikey=9a268985");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawMovies = JsonConvert.DeserializeObject<OMDBSearchResponse>(stringResult);
                    return rawMovies;
                }
                catch (HttpRequestException)
                {
                    return new OMDBSearchResponse();
                }
            }
        }

        /// <summary>
        /// Gets a Movie from IMDB by IMDBID
        /// </summary>
        /// <param name="imdbId">IMDB ID</param>
        /// <returns>Returns a Movie Description</returns>
        public async Task<MovieDescription> GetMovieExternal(string imdbId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://www.omdbapi.com/");
                    var response = await client.GetAsync($"?i={imdbId}&apikey=9a268985");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawMovies = JsonConvert.DeserializeObject<MovieDescription>(stringResult);
                    return rawMovies;
                }
                catch (HttpRequestException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Attempts to Get Movie from Custom API Database; if not found, Creates Movie from OMDB
        /// </summary>
        /// <param name="imdbId">IMDBID</param>
        /// <param name="loadSpoilers">Spoilers associated with Movie</param>
        /// <returns>Returns a Movie</returns>
        public async Task<Movie> GetMovieOrCreate(string imdbId, bool loadSpoilers = false)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.ID == imdbId);

            // If movie isn't in our database, try to get it from external api
            if (movie == null)
            {
                MovieDescription mDescripton = await GetMovieExternal(imdbId);

                if (mDescripton != null)
                {
                    movie = new Movie { ID = mDescripton.ImdbID, Title = mDescripton.Title, Genre = mDescripton.Genre, Plot = mDescripton.Plot, Year = int.Parse(mDescripton.Year), Poster = mDescripton.Poster };
                    _context.Movies.Add(movie);
                    await _context.SaveChangesAsync();
                }
            }

            if (movie != null && loadSpoilers)
            {
                movie.Spoilers = await _context.Spoilers.Where(x => x.MovieID == movie.ID).ToListAsync();
            }
            return movie;
        }
    }
}

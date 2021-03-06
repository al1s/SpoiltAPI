﻿using SpoiltAPI.Models.Interfaces;
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
        /// <summary>
        /// The context
        /// </summary>
        private readonly SpoiltContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MovieService(SpoiltContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves the movies.
        /// </summary>
        /// <returns>Returns a list of Movies</returns>
        public IEnumerable<Movie> RetrieveMovies(int pageNumber = 1, int pageSize = 15)
        {
            return _context.Movies.OrderByDescending(s => s.Spoilers.Count()).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
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
                    await UpdateMovieData(rawMovies);

                    return rawMovies;
                }
                catch (HttpRequestException)
                {
                    return new OMDBSearchResponse();
                }
            }
        }


        /// <summary>
        /// Updates the movie data.
        /// </summary>
        /// <param name="sr">The sr.</param>
        /// <returns></returns>
        private async Task UpdateMovieData(OMDBSearchResponse sr)
        {
            foreach (var item in sr.Search)
            {
                var movie = await GetMovieOrCreate(item.ImdbID,false);
                if(movie != null)
                {
                    item.Plot = movie.Plot;
                    item.Genre = movie.Genre;
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

                    movie = new Movie { ID = mDescripton.ImdbID, Title = mDescripton.Title, Genre = mDescripton.Genre, Plot = mDescripton.Plot, Year = mDescripton.Year, Poster = mDescripton.Poster };
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

        /// <summary>
        /// Checks the movie exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns true if a Movie exists and false if a Movie does not exist</returns>
        public bool CheckMovieExists(string id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }
    }
}

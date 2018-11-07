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

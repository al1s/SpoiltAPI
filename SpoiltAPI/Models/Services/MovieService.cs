using SpoiltAPI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using SpoiltAPI.Models.ViewModels;

namespace SpoiltAPI.Models.Services
{
    public class MovieService : IMovie
    {
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
    }
}

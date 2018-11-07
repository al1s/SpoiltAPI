using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpoiltAPI.Models.ViewModels
{
    public class OMDBSearchResponse
    {
        public string Name { get; set; }

        public IEnumerable<MovieSearchDescription> Search { get; set; }

        public bool Response { get; set; }

        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }

        public string Error { get; set; }
    }

    public class MovieSearchDescription
    {
        public string Title { get; set; }

        public string Year { get; set; }

        [JsonProperty("imdbID")]
        public string ImdbID { get; set; }

        public string Type { get; set; }

        public string Poster { get; set; }
    }

    public class MovieDescription
    {
        public string Title { get; set; }

        public string Year { get; set; }

        [JsonProperty("imdbID")]
        public string ImdbID { get; set; }

        public string Type { get; set; }

        public string Genre { get; set; }

        public string Poster { get; set; }

        public string Plot { get; set; }
    }
}

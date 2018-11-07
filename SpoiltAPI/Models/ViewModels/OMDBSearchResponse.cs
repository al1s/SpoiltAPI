using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpoiltAPI.Models.ViewModels
{
    public class OMDBSearchResponse
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>
        /// The search.
        /// </value>
        public IEnumerable<MovieSearchDescription> Search { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OMDBSearchResponse"/> is response.
        /// </summary>
        /// <value>
        ///   <c>true</c> if response; otherwise, <c>false</c>.
        /// </value>
        public bool Response { get; set; }

        /// <summary>
        /// Gets or sets the total results.
        /// </summary>
        /// <value>
        /// The total results.
        /// </value>
        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }
    }

    public class MovieSearchDescription
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets the imdb identifier.
        /// </summary>
        /// <value>
        /// The imdb identifier.
        /// </value>
        [JsonProperty("imdbID")]
        public string ImdbID { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the poster.
        /// </summary>
        /// <value>
        /// The poster.
        /// </value>
        public string Poster { get; set; }
    }

    public class MovieDescription
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets the imdb identifier.
        /// </summary>
        /// <value>
        /// The imdb identifier.
        /// </value>
        [JsonProperty("imdbID")]
        public string ImdbID { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        /// <value>
        /// The genre.
        /// </value>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the poster.
        /// </summary>
        /// <value>
        /// The poster.
        /// </value>
        public string Poster { get; set; }

        /// <summary>
        /// Gets or sets the plot.
        /// </summary>
        /// <value>
        /// The plot.
        /// </value>
        public string Plot { get; set; }
    }
}

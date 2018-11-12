using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SpoiltAPI.Models
{
    //[JsonObject(IsReference = true)]
    public class Movie
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }

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
        /// Gets or sets the genre.
        /// </summary>
        /// <value>
        /// The genre.
        /// </value>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the plot.
        /// </summary>
        /// <value>
        /// The plot.
        /// </value>
        public string Plot { get; set; }

        /// <summary>
        /// Gets or sets the poster.
        /// </summary>
        /// <value>
        /// The poster.
        /// </value>
        public string Poster { get; set; }

        /// <summary>
        /// Gets or sets the spoilers.
        /// </summary>
        /// <value>
        /// The spoilers.
        /// </value>
        public virtual ICollection<Spoiler> Spoilers { get; set; }
    }
}

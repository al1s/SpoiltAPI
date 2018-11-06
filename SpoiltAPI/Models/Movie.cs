using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SpoiltAPI.Models
{
    //[JsonObject(IsReference = true)]
    public class Movie
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string IMDBID { get; set; }

        public virtual ICollection<Spoiler> Spoilers { get; set; }


    }
}

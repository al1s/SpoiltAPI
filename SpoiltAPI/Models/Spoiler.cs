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
    public class Spoiler
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string SpoilerText { get; set; }
        public int Votes { get; set; }
        public DateTime Created { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string MovieID { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

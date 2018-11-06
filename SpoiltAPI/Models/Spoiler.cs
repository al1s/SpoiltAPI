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
    //[DataContract(IsReference = true)]
    public class Spoiler
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string SpoilerText { get; set; }
        public int Votes { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; set; }
        public int MovieID { get; set; }

        [JsonIgnore]
        public virtual Movie Movie { get; set; }
    }
}

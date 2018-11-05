using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpoiltAPI.Models
{
    public class Spoiler
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string SpoilerText { get; set; }
        public int Votes { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; set; }
        public int MovieID { get; set; }

        public Movie Movie { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpoiltAPI.Data
{
    public class SpoiltContext : DbContext
    {
        public SpoiltContext(DbContextOptions<SpoiltContext> options)
          : base(options)
        {
        }
    }
}

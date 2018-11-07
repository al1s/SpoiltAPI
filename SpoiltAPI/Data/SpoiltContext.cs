using Microsoft.EntityFrameworkCore;
using SpoiltAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpoiltAPI.Data
{
    public class SpoiltContext : DbContext
    {
        public SpoiltContext(DbContextOptions<SpoiltContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Spoiler> Spoilers { get; set; }

        /// <summary>
        /// Creates seed values for the database
        /// </summary>
        /// <param name="modelBuilder">Takes in a ModelBuilder object</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spoiler>()
            .Property(b => b.Created)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Title = "The Sixth Sense",
                    Year = 1999,
                    Genre = "Drama, Thriller, Mystery",
                    Plot = "A boy who communicates with spirits seeks the help of a disheartened child psychologist.",
                    Poster = "https://m.media-amazon.com/images/M/MV5BMWM4NTFhYjctNzUyNi00NGMwLTk3NTYtMDIyNTZmMzRlYmQyXkEyXkFqcGdeQXVyMTAwMzUyOTc@._V1_SX300.jpg",
                    ID = "tt0167404",
                });
            modelBuilder.Entity<Spoiler>().HasData(
                new Spoiler
                {
                    ID = 1,
                    UserName = "Stairmaster",
                    SpoilerText = "Bruce Willis was DEAD THE WHOLE TIME!!!!!",
                    Votes = -45,
                    MovieID = "tt0167404",
                });
        }
    }
}

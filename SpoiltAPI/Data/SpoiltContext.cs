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
        /// <summary>
        /// Initializes a new instance of the <see cref="SpoiltContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SpoiltContext(DbContextOptions<SpoiltContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the movies.
        /// </summary>
        /// <value>
        /// The movies.
        /// </value>
        public DbSet<Movie> Movies { get; set; }
        /// <summary>
        /// Gets or sets the spoilers.
        /// </summary>
        /// <value>
        /// The spoilers.
        /// </value>
        public DbSet<Spoiler> Spoilers { get; set; }

        /// <summary>
        /// Creates seed values for the database
        /// </summary>
        /// <param name="modelBuilder">Takes in a ModelBuilder object</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Auto populate date.
            modelBuilder.Entity<Spoiler>()
            .Property(b => b.Created)
            .HasDefaultValueSql("getdate()");

            // Seed data.
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
                    MovieID = "tt0167404",
                });
        }
    }
}

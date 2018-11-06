using System;
using Xunit;
using static SpoiltAPI.Program;
using SpoiltAPI.Models;
using SpoiltAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using SpoiltAPI.Data;

namespace SpoiltAPITests
{
    public class UnitTest1
    {
        /// <summary>
        /// Tests Setters and Getters on Movie Model
        /// </summary>
        [Fact]
        public void CanSetAndGetMovieTitle()
        {
            Movie movie = new Movie();
            movie.Title = "Dune";

            Assert.Equal("Dune", movie.Title);
        }

        /// <summary>
        /// Tests Setters and Getters on Spoiler Model
        /// </summary>
        [Fact]
        public void CanSetAndGetSpoilerText()
        {
            Spoiler spoiler = new Spoiler();
            spoiler.SpoilerText = "Spoilers!";

            Assert.Equal("Spoilers!", spoiler.SpoilerText);
        }

        /// <summary>
        /// Tests that a Movie can be Created and Read
        /// </summary>
        [Fact]
        public async void CanCreateAndReadMovie()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("GetMovieName")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Movie movie = new Movie();
                movie.Title = "Dune";

                context.Movies.Add(movie);
                context.SaveChanges();

                var movieTitle = await context.Movies.FirstOrDefaultAsync(x => x.Title == movie.Title);

                Assert.Equal("Dune", movieTitle.Title);
            }
        }

        /// <summary>
        /// Tests that a Movie can be Updated
        /// </summary>
        [Fact]
        public async void CanUpdateMovie()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("GetMovieNameUpdate")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Movie movie = new Movie();
                movie.Title = "Dune";

                context.Movies.Add(movie);
                context.SaveChanges();

                movie.Title = "BladeRunner";

                context.Movies.Update(movie);
                context.SaveChanges();

                var movieTitle = await context.Movies.FirstOrDefaultAsync(x => x.Title == movie.Title);

                Assert.Equal("BladeRunner", movieTitle.Title);
            }
        }

        /// <summary>
        /// Tests that a Movie can be Deleted
        /// </summary>
        [Fact]
        public async void CanDeleteMovie()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("GetMovieNameDelete")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Movie movie = new Movie();
                movie.Title = "Dune";

                context.Movies.Add(movie);
                context.SaveChanges();

                context.Movies.Remove(movie);
                context.SaveChanges();

                var movieTitle = await context.Movies.ToListAsync();

                Assert.DoesNotContain(movie, movieTitle);
            }
        }

        /// <summary>
        /// Tests that a Spoiler can be Created and Read
        /// </summary>
        [Fact]
        public async void CanCreateAndReadSpoiler()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("GetSpoilerText")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Spoiler spoiler = new Spoiler();
                spoiler.SpoilerText = "Spoilers!";

                context.Spoilers.Add(spoiler);
                context.SaveChanges();

                var spoilerText = await context.Spoilers.FirstOrDefaultAsync(x => x.SpoilerText == spoiler.SpoilerText);

                Assert.Equal("Spoilers!", spoilerText.SpoilerText);
            }
        }
        
        /// <summary>
        /// Tests that a Spoiler can be Updated
        /// </summary>
        [Fact]
        public async void CanUpdateSpoiler()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("GetSpoilerTextUpdate")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Spoiler spoiler = new Spoiler();
                spoiler.SpoilerText = "Spoilers!";

                context.Spoilers.Add(spoiler);
                context.SaveChanges();

                spoiler.SpoilerText = "NoSpoilers!";

                context.Spoilers.Update(spoiler);
                context.SaveChanges();

                var spoilerText = await context.Spoilers.FirstOrDefaultAsync(x => x.SpoilerText == spoiler.SpoilerText);

                Assert.Equal("NoSpoilers!", spoilerText.SpoilerText);
            }
        }

        /// <summary>
        /// Tests that a Spoiler can be Deleted
        /// </summary>
        [Fact]
        public async void CanDeleteSpoiler()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("GetSpoilerTextDelete")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Spoiler spoiler = new Spoiler();
                spoiler.SpoilerText = "Spoilers!";

                context.Spoilers.Add(spoiler);
                context.SaveChanges();

                context.Spoilers.Remove(spoiler);
                context.SaveChanges();

                var spoilerText = await context.Spoilers.ToListAsync();

                Assert.DoesNotContain(spoiler, spoilerText);
            }
        }
    }
}

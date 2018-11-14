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
        /// Tests Setters on Movie Model
        /// </summary>
        [Fact]
        public void CanSetMovieTitle()
        {
            Movie movie = new Movie();
            movie.Title = "Dune";

            Assert.Equal("Dune", movie.Title);
        }

        /// <summary>
        /// Determines whether this instance [can get movie title].
        /// </summary>
        [Fact]
        public void CanGetMovieTitle()
        {
            Movie movie = new Movie() { Title = "Dune" };
            Assert.Equal("Dune", movie.Title);
        }

        /// <summary>
        /// Determines whether this instance [can set movie plot].
        /// </summary>
        [Fact]
        public void CanSetMoviePlot()
        {
            Movie movie = new Movie();
            movie.Plot = "This is a plot";

            Assert.Equal("This is a plot", movie.Plot);
        }

        /// <summary>
        /// Determines whether this instance [can get movie plot].
        /// </summary>
        [Fact]
        public void CanGetMoviePlot()
        {
            Movie movie = new Movie() { Plot = "This is a plot" };
            Assert.Equal("This is a plot", movie.Plot);
        }

        /// <summary>
        /// Determines whether this instance [can set movie poster].
        /// </summary>
        [Fact]
        public void CanSetMoviePoster()
        {
            Movie movie = new Movie();
            movie.Poster = "poster.jpg";

            Assert.Equal("poster.jpg", movie.Poster);
        }

        /// <summary>
        /// Determines whether this instance [can get movie poster].
        /// </summary>
        [Fact]
        public void CanGetMoviePoster()
        {
            Movie movie = new Movie() { Poster = "poster.jpg" };
            Assert.Equal("poster.jpg", movie.Poster);
        }

        /// <summary>
        /// Determines whether this instance [can set movie year].
        /// </summary>
        [Fact]
        public void CanSetMovieYear()
        {
            Movie movie = new Movie();
            movie.Year = "1992-1993";

            Assert.Equal("1992-1993", movie.Year);
        }

        /// <summary>
        /// Determines whether this instance [can get movie year].
        /// </summary>
        [Fact]
        public void CanGetMovieYear()
        {
            Movie movie = new Movie() { Year = "1992-1993" };
            Assert.Equal("1992-1993", movie.Year);
        }

        /// <summary>
        /// Determines whether this instance [can set movie genre].
        /// </summary>
        [Fact]
        public void CanSetMovieGenre()
        {
            Movie movie = new Movie();
            movie.Genre = "Horror, Romance";

            Assert.Equal("Horror, Romance", movie.Genre);
        }

        /// <summary>
        /// Determines whether this instance [can get movie genre].
        /// </summary>
        [Fact]
        public void CanGetMovieGenre()
        {
            Movie movie = new Movie() { Genre = "Horror, Romance" };
            Assert.Equal("Horror, Romance", movie.Genre);
        }


        /// <summary>
        /// Tests Setters and Getters on Spoiler Model
        /// </summary>
        [Fact]
        public void CanSetSpoilerText()
        {
            Spoiler spoiler = new Spoiler();
            spoiler.SpoilerText = "Spoilers!";

            Assert.Equal("Spoilers!", spoiler.SpoilerText);
        }

        /// <summary>
        /// Determines whether this instance [can get spoiler text].
        /// </summary>
        [Fact]
        public void CanGetSpoilerText()
        {
            Spoiler spoiler = new Spoiler() { SpoilerText = "Spoilers!" };

            Assert.Equal("Spoilers!", spoiler.SpoilerText);
        }

        /// <summary>
        /// Determines whether this instance [can set spoiler user name].
        /// </summary>
        [Fact]
        public void CanSetSpoilerUserName()
        {
            Spoiler spoiler = new Spoiler();
            spoiler.UserName = "JamesSmith";

            Assert.Equal("JamesSmith", spoiler.UserName);
        }

        /// <summary>
        /// Determines whether this instance [can get spoiler user name].
        /// </summary>
        [Fact]
        public void CanGetSpoilerUserName()
        {
            Spoiler spoiler = new Spoiler() { UserName = "JamesSmith" };

            Assert.Equal("JamesSmith", spoiler.UserName);
        }



        /// <summary>
        /// Tests that a Spoiler can be Created
        /// </summary>
        [Fact]
        public async void CanCreateSpoiler()
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
                await context.SaveChangesAsync();

                Assert.True(await context.Spoilers.AnyAsync(x => x.ID == spoiler.ID));
            }
        }

        /// <summary>
        /// Determines whether this instance [can read spoiler].
        /// </summary>
        [Fact]
        public async void CanReadSpoiler()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("CanReadSpoiler")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Spoiler spoiler = new Spoiler();
                spoiler.SpoilerText = "Spoilers!";

                context.Spoilers.Add(spoiler);
                await context.SaveChangesAsync();

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
                .UseInMemoryDatabase("CanUpdateSpoiler")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Spoiler spoiler = new Spoiler();
                spoiler.SpoilerText = "Spoilers!";

                context.Spoilers.Add(spoiler);
                await context.SaveChangesAsync();

                spoiler.SpoilerText = "NoSpoilers!";

                context.Spoilers.Update(spoiler);
                await context.SaveChangesAsync();

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
                .UseInMemoryDatabase("SpoilerDelete")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Spoiler spoiler = new Spoiler();
                spoiler.SpoilerText = "Spoilers!";

                context.Spoilers.Add(spoiler);
                await context.SaveChangesAsync();

                context.Spoilers.Remove(spoiler);
                await context.SaveChangesAsync();

                var spoilerText = await context.Spoilers.ToListAsync();

                Assert.DoesNotContain(spoiler, spoilerText);
            }
        }

        /// <summary>
        /// Determines whether this instance [can create movie].
        /// </summary>
        [Fact]
        public async void CanCreateMovie()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("CanCreateMovie")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Movie movie = new Movie();
                movie.ID = "i231231";
                context.Movies.Add(movie);
                await context.SaveChangesAsync();

                Assert.True(await context.Movies.AnyAsync(x => x.ID == movie.ID));
            }
        }

        /// <summary>
        /// Determines whether this instance [can read movie].
        /// </summary>
        [Fact]
        public async void CanReadMovie()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("CanReadMovie")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Movie movie = new Movie();
                movie.ID = "i231231";
                movie.Title = "My Movie";
                context.Movies.Add(movie);

                await context.SaveChangesAsync();

                var movieTitle = await context.Movies.FirstOrDefaultAsync(x => x.ID == movie.ID);

                Assert.Equal("My Movie", movieTitle.Title);
            }
        }

        /// <summary>
        /// Determines whether this instance [can update movie].
        /// </summary>
        [Fact]
        public async void CanUpdateMovie()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("CanUpdateMovie")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Movie movie = new Movie();
                movie.ID = "i231231";
                movie.Title = "My Movie";
                context.Movies.Add(movie);

                context.SaveChanges();

                movie.Title = "NO TITLE";

                context.Movies.Update(movie);
                await context.SaveChangesAsync();

                var movieTitle = await context.Movies.FirstOrDefaultAsync(x => x.ID == movie.ID);

                Assert.Equal("NO TITLE", movieTitle.Title);
            }
        }

        /// <summary>
        /// Determines whether this instance [can delete movie].
        /// </summary>
        [Fact]
        public async void CanDeleteMovie()
        {
            DbContextOptions<SpoiltContext> options =
                new DbContextOptionsBuilder<SpoiltContext>()
                .UseInMemoryDatabase("CanDeleteMovie")
                .Options;

            using (SpoiltContext context = new SpoiltContext(options))
            {
                Movie movie = new Movie();
                movie.ID = "i231231";
                movie.Title = "My Movie";

                context.Movies.Add(movie);

                context.SaveChanges();

                context.Movies.Remove(movie);
                await context.SaveChangesAsync();

                var moviesText = await context.Movies.ToListAsync();

                Assert.DoesNotContain(movie, moviesText);
            }
        }
    }
}

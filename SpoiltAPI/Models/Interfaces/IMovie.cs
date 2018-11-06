using SpoiltAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpoiltAPI.Models.Interfaces
{
    public interface IMovie
    {
        Task<OMDBSearchResponse> SearchMovie(string term);

        Task<MovieDescription> GetMovieExternal(string imdbId);
    }
}

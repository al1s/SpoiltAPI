using SpoiltAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpoiltAPI.Models.Interfaces
{
    interface IMovie
    {
        Task<OMDBSearchResponse> SearchMovie(string term);
    }
}

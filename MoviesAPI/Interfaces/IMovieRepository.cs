using MoviesAPI.DbModels;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Interfaces 
{
    public interface IMovieRepository
    {
        MovieResponse GetMovie(int id);
        IEnumerable<MovieResponse> GetAllMovies();
        MovieResponse Add(MovieRequest movie);
        MovieResponse Update(MovieUpdateVM updatedMovie);
        MovieResponse Delete(int id);
    }
}

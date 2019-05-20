using MoviesAPI.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Interfaces 
{
    public interface IMovieRepository
    {
        Movie GetMovie(int id);
        IEnumerable<Movie> GetAllMovies();
        Movie Add(Movie movie);
        Movie Update(Movie updatedMovie);
        Movie Delete(int id);
    }
}

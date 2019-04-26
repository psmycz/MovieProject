using System.Collections.Generic;
using System.Linq;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;

namespace MoviesAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private static MoviesService _instance;
        private List<Movie> _movies;

        public MoviesService()
        {
            _movies = new List<Movie>
            {
                new Movie()
                {
                    Id = 1,
                    Title = "Super film",
                    Year = 1998,
                 },
                new Movie()
                {
                    Id = 2,
                    Title = "Super film2",
                    Year = 1998,
                 },
            };
        }

        public static MoviesService Instace
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MoviesService();
                }

                return _instance;
            }
        }

        public List<Movie> GetAll()
        {
            return _movies;
        }

        public Movie GetById(int id)
        {
            Movie foundMovie = _movies
                  .Where(movie => movie.Id == id)
                  .SingleOrDefault();

            return foundMovie;
        }

        public void AddNewMovie(Movie movie)
        {
            _movies.Add(movie);
        }

        public bool UpdateMovie(Movie movie)
        {
            Movie foundMovie = GetById(movie.Id);

            if (foundMovie == null)
            {
                return false;
            }

            foundMovie.Title = movie.Title;
            foundMovie.Year = movie.Year;

            return true;
        }

        public void Remove(int movieId)
        {
            Movie movie = GetById(movieId);
            _movies.Remove(movie);
        }
    }
}

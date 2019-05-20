using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;

namespace MoviesAPI.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieAPIDbContext context;

        public MovieRepository(MovieAPIDbContext context)
        {
            this.context = context;
        }

        public Movie Add(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
            return movie;
        }

        public Movie Delete(int id)
        {
            Movie movie = context.Movies.Find(id);
            if (movie != null)
            {
                context.Movies.Remove(movie);
                context.SaveChanges();
            }
            return movie;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return context.Movies;
        }

        public Movie GetMovie(int id)
        {
            return context.Movies.Find(id);
        }

        public Movie Update(Movie updatedMovie)
        {
            var movie = context.Movies.Attach(updatedMovie);
            movie.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedMovie;
        }
    }
}

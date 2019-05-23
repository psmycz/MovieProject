using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class MovieRepository : IMovieRepository
    {
                                                        // normalnie w tej klasie powinno się wywoływać dispose, aby usuwać 
                                                        // połączenie z bazą po wykonanym requescie, ale za nas robi to startup, 
                                                        // gdzie dodaliśmy instancję klasy jako AddScoped
        private readonly MovieAPIDbContext context;

        public MovieRepository(MovieAPIDbContext context)
        {
            this.context = context;
        }

        public MovieResponse Add(MovieRequest movieRequest)
        {

            Movie movie = new Movie()
            {
                Title = movieRequest.Title,
                Year = movieRequest.Year,
            };

            var existingDirectors = context.Directors.Select(d => d.DirectorId).ToList();
            if (movieRequest.DirectorId > 0)
            {
                if (existingDirectors.Contains(movieRequest.DirectorId))
                    movie.DirectorId = movieRequest.DirectorId;
            }

            context.Movies.Add(movie);
            context.SaveChanges();

            
            if (movieRequest.Genres != null) {

                var existingGenres = context.Genres.Select(g => g.GenreId).ToList();
                foreach (var genreId in movieRequest.Genres)
                {
                    if (existingGenres.Contains(genreId))
                    {
                        context.MovieGenres.Add(new MovieGenre() { MovieId = movie.Id, GenreId = genreId });
                        context.SaveChanges();
                    }
                }
            }

            MovieResponse movieResponse = GetMovie(movie.Id);
            
            return movieResponse;
        }

        public MovieResponse Delete(int id)
        {
            Movie movie = context.Movies.Find(id);
            if (movie == null)
                return null;

            MovieResponse movieResponse = GetMovie(id);

            context.Movies.Remove(movie);
            context.SaveChanges();

            return movieResponse;
        }

        public IEnumerable<MovieResponse> GetAllMovies()
        {
            IEnumerable<MovieResponse> movies = (from m in context.Movies
                                                 select new MovieResponse
                                                 {
                                                     Id = m.Id,
                                                     Title = m.Title,
                                                     Year = m.Year,
                                                     UsersRating = m.UsersRating,
                                                     Director = (m.DirectorId != null) ? m.Director : null,
                                                     Genres = context.GetDataForMovie.Where(a => a.Id == m.Id).Select(a => a.GenreName).Distinct().ToList(),
                                                     Reviews = m.MReviews.ToList(),//AutoMapper.Mapper.Map<List<ReviewResponse>>(m.MReviews),
                                                     Users = m.MovieUsers.Select(mu => mu.User).ToList()
                                                 });
            return movies;
        }

        public MovieResponse GetMovie(int id)
        {
            MovieResponse movieResponse = (from m in context.Movies
                                                 where m.Id == id
                                                 select new MovieResponse
                                                 {
                                                     Id = m.Id,
                                                     Title = m.Title,
                                                     Year = m.Year,
                                                     UsersRating = m.UsersRating,
                                                     Director = (m.DirectorId != null) ? m.Director : null,
                                                     Genres = context.GetDataForMovie.Where(a => a.Id == m.Id).Select(a => a.GenreName).Distinct().ToList(),
                                                     Reviews = m.MReviews.ToList(),//AutoMapper.Mapper.Map<List<ReviewResponse>>(m.MReviews),
                                                     Users = m.MovieUsers.Select(mu => mu.User).ToList()
                                                 }).SingleOrDefault();
            return movieResponse;
        }

        public MovieResponse Update(MovieUpdateVM updatedMovie)
        {
            var movie = context.Movies.Find(updatedMovie.Id);
            if (movie == null)
                return null;

            movie.Title = updatedMovie.Title;
            movie.Year = updatedMovie.Year;

            var existingDirectors = context.Directors.Select(d => d.DirectorId).ToList();
            if (updatedMovie.DirectorId > 0 && existingDirectors.Contains(updatedMovie.DirectorId))
            {
                movie.DirectorId = updatedMovie.DirectorId;
            }

            context.MovieGenres.RemoveRange(context.MovieGenres.Where(mg => mg.MovieId == updatedMovie.Id));
            context.SaveChanges();

            if (updatedMovie.Genres != null)
            {
                var existingGenres = context.Genres.Select(g => g.GenreId).ToList();
                foreach (var genreId in updatedMovie.Genres)
                {
                    if (existingGenres.Contains(genreId))
                    {
                        context.MovieGenres.Add(new MovieGenre() { MovieId = movie.Id, GenreId = genreId });
                        context.SaveChanges();
                    }
                }
            }

            return GetMovie(movie.Id);
        }
    }
}

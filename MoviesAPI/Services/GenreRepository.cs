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
    public class GenreRepository : IGenreRepository
    {
        private readonly MovieAPIDbContext context;

        public GenreRepository(MovieAPIDbContext context)
        {
            this.context = context;
        }

        public GenreResponse Add(GenreRequest genreRequest)
        {

            Genre genre = new Genre()
            {
                GenreName = genreRequest.GenreName
            };

            context.Genres.Add(genre);
            context.SaveChanges();

            GenreResponse genreResponse = GetGenre(genre.GenreId);
            
            return genreResponse;
        }

        public GenreResponse Delete(int id)
        {
            Genre genre= context.Genres.Find(id);
            if (genre == null)
                return null;
            context.MovieGenres.RemoveRange(genre.MovieGenres);

            GenreResponse genreResponse = GetGenre(id);

            context.Genres.Remove(genre);
            context.SaveChanges();

            return genreResponse;
        }

        public IEnumerable<GenreResponse> GetAllGenres()
        {
            IEnumerable<GenreResponse> genres = (from g in context.Genres
                                                 select new GenreResponse
                                                 {
                                                     GenreId = g.GenreId,
                                                     GenreName = g.GenreName,
                                                     Movies = g.MovieGenres.Select(mg => mg.Movie).ToList()
                                                 });
            return genres;
        }

        public GenreResponse GetGenre(int id)
        {
            GenreResponse genreResponse = (from g in context.Genres
                                           where g.GenreId == id
                                           select new GenreResponse
                                           {
                                               GenreId = g.GenreId,
                                               GenreName = g.GenreName,
                                               Movies = g.MovieGenres.Select(mg => mg.Movie).ToList()
                                           }).SingleOrDefault();
            return genreResponse;
        }

        public GenreResponse Update(GenreUpdateVM updatedGenre)
        {
            var genre = context.Genres.Find(updatedGenre.GenreId);
            if (genre == null)
                return null;

            genre.GenreName= updatedGenre.GenreName;

            context.SaveChanges();

            return GetGenre(genre.GenreId);
        }
    }
}

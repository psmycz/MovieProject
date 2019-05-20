using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DbModels
{
    public static class ModelBuilderExtensions
    {
        public static void EntityConfiguration(this ModelBuilder modelBuilder)  // fluent API
        {
            modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });     // many to many: movies -> genres

            modelBuilder.Entity<MovieUser>().HasKey(mu => new { mu.MovieId, mu.UserId });       // many to many: movies -> users

            modelBuilder.Entity<Movie>()            // one to many:  one director -> many movies 
                .HasOne<Director>(m => m.Director)
                .WithMany(d => d.DMovies)
                .HasForeignKey(m => m.DirectorId);

            modelBuilder.Entity<Movie>()            // one to many: one movie -> many reviews
                .HasMany<Review>(m => m.MReviews)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<Movie>()            // one to many: one movie -> many movieGenres
                .HasMany<MovieGenre>(m => m.MovieGenres)
                .WithOne(mg => mg.Movie)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<Movie>()            // one to many: one movie -> many movieUsers
                .HasMany<MovieUser>(m => m.MovieUsers)
                .WithOne(mu => mu.Movie)
                .HasForeignKey(mu => mu.MovieId);

            modelBuilder.Entity<User>()             // one to many: one user -> many reviews
                .HasMany<Review>(u => u.UReviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<User>()             // one to many: one user -> many movieUsers
               .HasMany<MovieUser>(u => u.MovieUsers)
               .WithOne(mu => mu.User)
               .HasForeignKey(mu => mu.UserId);

            modelBuilder.Entity<Genre>()            // one to many: one genre -> many movies
                .HasMany<MovieGenre>(g => g.MovieGenres)
                .WithOne(mg => mg.Genre)
                .HasForeignKey(mg => mg.GenreId);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {

            List<Movie> movies = new List<Movie>{
                    new Movie { Id = 1, Title = "Titanic", Year = 1997, DirectorId = 1 }, //jamescameron
                    new Movie { Id = 2, Title = "Aladyn", Year = 2019, DirectorId = 2 }, // Guy Ritchie
                    new Movie { Id = 3, Title = "Król Lew", Year = 2019, DirectorId = 3 }, // Jon Favreau
                    new Movie { Id = 4, Title = "Gladiator", Year = 2000, DirectorId = 4 }, // Ridley Scott
                    new Movie { Id = 5, Title = "Hannibal", Year = 2001, DirectorId = 4 } // Ridley Scott
            };

            List<Director> directors = new List<Director>{
                    new Director { DirectorId = 1, Name = "James", Surname = "Cameron" },
                    new Director { DirectorId = 2, Name = "Guy", Surname = "Ritchie" },
                    new Director { DirectorId = 3, Name = "Jon", Surname = "Favreau" },
                    new Director { DirectorId = 4, Name = "Ridley", Surname = "Scott" }
            };

            List<Review> reviews = new List<Review>{
                    new Review { Id = 1, Comment = "Bardzo fajny film."             , Rate = 5, UserId = 1, MovieId = 2},   // Piotrek
                    new Review { Id = 2, Comment = "Kompletnie mi się nie podobał." , Rate = 1, UserId = 1, MovieId = 1 },  // Piotrek
                    new Review { Id = 3, Comment = "Przeciętny."                    , Rate = 3, UserId = 2, MovieId = 4 },  // Rafał
                    new Review { Id = 4, Comment = "Kiepski film."                  , Rate = 3, UserId = 2, MovieId = 5 },  // Rafał
                    new Review { Id = 5, Comment = "Bardzo słaby."                  , Rate = 2, UserId = 3, MovieId = 5 },  // Adam
            };

            List<User> users = new List<User>{
                    new User { UserId = 1, Name = "Piotrek", Surname = "Kowalski" },
                    new User { UserId = 2, Name = "Rafał", Surname = "Mazurek" },
                    new User { UserId = 3, Name = "Adam", Surname = "Nowak" }
            };

            List<Genre> genres = new List<Genre>{
                    new Genre { GenreId = 1, GenreName = "Akcja"},
                    new Genre { GenreId = 2, GenreName = "Komedia" },
                    new Genre { GenreId = 3, GenreName = "Tragedia" },
                    new Genre { GenreId = 4, GenreName = "Romantyczny" }
            };

            List<MovieGenre> movieGenres = new List<MovieGenre>{
                new MovieGenre { GenreId = 2, MovieId = 3},
                new MovieGenre { GenreId = 1, MovieId = 3},
                new MovieGenre { GenreId = 3, MovieId = 1},
                new MovieGenre { GenreId = 4, MovieId = 3},
                new MovieGenre { GenreId = 1, MovieId = 5}
            };

            List<MovieUser> movieUsers = new List<MovieUser>{
                new MovieUser { UserId = 1, MovieId = 1},
                new MovieUser { UserId = 1, MovieId = 2},
                new MovieUser { UserId = 1, MovieId = 3},
                new MovieUser { UserId = 2, MovieId = 4},
                new MovieUser { UserId = 2, MovieId = 5},
                new MovieUser { UserId = 3, MovieId = 5},
                new MovieUser { UserId = 3, MovieId = 2},
                new MovieUser { UserId = 3, MovieId = 1}
            };

            // relacje
            // nie zaimplementowane narazie w ten sposob..

/*         foreach (var m in movies) {
                m.Director = directors.Find(d => d.DirectorId == m.DirectorId);
                m.MReviews = reviews.FindAll(r => r.MovieId == m.Id);
                m.MovieGenres = movieGenres.FindAll(mg => mg.MovieId == m.Id);
                m.MovieUsers = movieUsers.FindAll(mu => mu.MovieId == m.Id);
            }
            foreach (var d in directors){
                d.DMovies = movies.FindAll(m => m.DirectorId == d.DirectorId);
            }
            foreach(var r in reviews){
                r.Movie = movies.Find(m => m.Id == r.MovieId);
                r.User = users.Find(u => u.UserId == r.UserId);
                r.UserName = r.User.Name;
            }
            foreach (var u in users){
                u.UReviews = reviews.FindAll(r => r.UserId == u.UserId);
                u.MovieUsers = movieUsers.FindAll(mu => mu.UserId == u.UserId);
            }
            foreach(var g in genres){
                g.MovieGenres = movieGenres.FindAll(mg => mg.GenreId == g.GenreId);
            }
            foreach(var mg in movieGenres)
            {
                mg.Genre = genres.Find(g => g.GenreId == mg.GenreId);
                mg.Movie = movies.Find(m => m.Id == mg.MovieId);
            }
            foreach (var mu in movieUsers)
            {
                mu.User = users.Find(u => u.UserId == mu.UserId);
                mu.Movie = movies.Find(m => m.Id == mu.MovieId);
            }
*/
            // faktyczny seed
            
            modelBuilder.Entity<Director>().HasData(directors);
            modelBuilder.Entity<Review>().HasData(reviews);
            modelBuilder.Entity<MovieGenre>().HasData(movieGenres);
            modelBuilder.Entity<MovieUser>().HasData(movieUsers);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Movie>().HasData(movies);

        }
    }
}

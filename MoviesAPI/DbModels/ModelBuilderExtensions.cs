using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DbModels
{
    public static class ModelBuilderExtensions
    {
        public static void Seed (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                    new Movie { Id = 1, Title = "Szczęki 1", Year = 2001 },
                    new Movie { Id = 2, Title = "Szczęki 2", Year = 2002 },
                    new Movie { Id = 3, Title = "Szczęki 3", Year = 2003 }
                );
        }

        public static void EntityConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });
            //modelBuilder.Entity<MovieGenre>()
            //    .HasOne(mg => mg.Movie)
            //    .WithMany(m => m.MovieGenres)
            //    .HasForeignKey(mg => mg.MovieId);
            //modelBuilder.Entity<MovieGenre>()
            //    .HasOne(mg => mg.Genre)
            //    .WithMany(g => g.GMovies)
            //    .HasForeignKey(mg => mg.GenreId);

            modelBuilder.Entity<MovieUser>().HasKey(mu => new { mu.MovieId, mu.UserId });

            // todo relacje dla reszty xd
        }

    }
}

using MoviesAPI.DbModels;
using System.Collections.Generic;

namespace MoviesAPI.Models
{
    public class MovieResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public double? UsersRating { get; set; }

        public Director Director { get; set; }

        public List<string> Genres { get; set; }

        public List<Review> Reviews { get; set; }

        public List<User> Users { get; set; }
    }
}

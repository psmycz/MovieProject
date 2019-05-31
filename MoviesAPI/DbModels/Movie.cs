using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DbModels
{
    public class Movie
    {
        public Movie()
        { 
            this.MovieReviews = new HashSet<Review>();
            this.MovieGenres = new HashSet<MovieGenre>();
            this.MovieUsers = new HashSet<MovieUser>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [Range(1900,2050)]
        public int Year { get; set; }

        public double? UsersRating { get; set; }

        [JsonIgnore]
        public int? DirectorId { get; set; }
        [JsonIgnore]
        public virtual Director Director { get; set; }
        [JsonIgnore]
        public virtual ICollection<Review> MovieReviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
        [JsonIgnore]
        public virtual ICollection<MovieUser> MovieUsers { get; set; }
    }
}

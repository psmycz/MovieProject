using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DbModels
{
    public class User
    {
        public User()
        {
            this.UReviews = new HashSet<Review>();
            this.MovieUsers = new HashSet<MovieUser>();
        }

        [Key]
        public int UserId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Surname { get; set; }

        [JsonIgnore]
        public virtual ICollection<Review> UReviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<MovieUser> MovieUsers { get; set; }
    }
}

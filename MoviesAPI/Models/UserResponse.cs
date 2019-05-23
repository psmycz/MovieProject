using MoviesAPI.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Movie> Movies{ get; set; }
    }
}

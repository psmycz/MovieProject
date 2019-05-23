using MoviesAPI.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class GenreResponse
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public List<Movie> Movies{ get; set; }
    }
}

using MoviesAPI.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class DirectorResponse
    {
        public int DirectorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Movie> Movies{ get; set; }
    }
}

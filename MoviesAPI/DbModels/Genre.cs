using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DbModels
{
    public class Genre
    {
        public Genre()
        {
            this.GMovies = new HashSet<MovieGenre>();
        }

        [Key]
        public int GenreId { get; set; }
        [Required][MaxLength(30)]
        public string GenreName { get; set; }

        public virtual ICollection<MovieGenre> GMovies { get; set; }
    }
}

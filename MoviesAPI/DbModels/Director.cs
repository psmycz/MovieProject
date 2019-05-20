using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DbModels
{
    public class Director
    {
        public Director()
        {
            this.DMovies = new HashSet<Movie>();
        }

        [Key]
        public int DirectorId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Surname { get; set; }

        public virtual ICollection<Movie> DMovies { get; set; }
    }
}
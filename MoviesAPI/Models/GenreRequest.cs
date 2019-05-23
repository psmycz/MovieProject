using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class GenreRequest
    {
        [Required]
        [MaxLength(30)]
        public string GenreName { get; set; }
    }
}

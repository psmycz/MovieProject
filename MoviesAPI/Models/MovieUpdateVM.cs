using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class MovieUpdateVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Range(1888, 2050, ErrorMessage = "Incorrect Year!")]
        public int Year { get; set; }

        public int DirectorId { get; set; }

        public List<int> Genres { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class ReviewUpdateVM
    {
        [Required]
        public int ReviewId { get; set; }

        [MinLength(20)]
        [MaxLength(150)]
        public string Comment { get; set; }

        [Required]
        [Range(1, 5)]
        public short Rate { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class ReviewRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [MinLength(20)]
        [MaxLength(150)]
        public string Comment { get; set; }

        [Required]
        [Range(1, 5)]
        public short Rate { get; set; }

    }
}

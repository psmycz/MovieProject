using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DbModels
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [MinLength(20)][MaxLength(150)]
        public string Comment { get; set; }

        [Required][Range(1,5)]
        public short Rate { get; set; }

        [Required]
        public int MovieId { get; set; }

        [JsonIgnore]
        public virtual Movie Movie { get; set; }

        public string UserName { get; set; }
        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}

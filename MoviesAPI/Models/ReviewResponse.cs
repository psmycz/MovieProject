using MoviesAPI.DbModels;

namespace MoviesAPI.Models
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public short Rate { get; set; }
        public string MovieTitle { get; set; }
        public User User { get; set; }
    }
}

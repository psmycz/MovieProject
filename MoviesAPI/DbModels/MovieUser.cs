namespace MoviesAPI.DbModels
{
    public class MovieUser
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
namespace MoviesAPI.DbModels
{
    public class MovieUser
    {
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
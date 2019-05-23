using System.Collections.Generic;
using System.Linq;
using MoviesAPI.Common;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;

namespace MoviesAPI.Services
{
    public class ReviewsService : IReviewsService
    {
        private MoviesContext _context;
        public ReviewsService(MoviesContext context)
        {
            _context = context;
        }

        public List<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }

        public Review GetById(int id)
        {
            return _context.Reviews.Find(id) ;
        }

        public void AddNewReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public bool UpdateReview(Review review)
        {
            Review foundReview = GetById(review.Id);

            if (foundReview == null)
            {
                return false;
            }

            foundReview.Comment = review.Comment;
            foundReview.MovieId = review.MovieId;
            foundReview.Rate = review.Rate;

            _context.SaveChanges();

            return true;
        }

        public void Remove(int reviewId)
        {
            _context.Remove(_context.Reviews.Find(reviewId));
            _context.SaveChanges();
        }
    }
}

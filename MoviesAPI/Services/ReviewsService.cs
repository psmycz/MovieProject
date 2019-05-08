using System.Collections.Generic;
using System.Linq;
using MoviesAPI.Common;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;

namespace MoviesAPI.Services
{
    public class ReviewsService : IReviewsService
    {
        private static ReviewsService _instance;
        private List<Review> _reviews;

        public ReviewsService()
        {
            _reviews = new List<Review>
            {
                new Review()
                {
                    Id = 1,
                    MovieId = 1,
                    Comment = "dobry film",
                    Rate = 5
                 },
                new Review()
                {
                    Id = 2,
                    MovieId = 1,
                    Comment = "nie warto oglądać",
                    Rate = 1
                 },
            };
        }

        public static ReviewsService Instace
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ReviewsService();
                }

                return _instance;
            }
        }

        public List<Review> GetAll()
        {
            return _reviews;
        }

        public Review GetById(int id)
        {
            Review foundMovie = _reviews
                  .Where(review => review.Id == id)
                  .SingleOrDefault();

            return foundMovie;
        }

        public void AddNewReview(Review review)
        {
            //TODO
            var movie = MoviesService.Instace.GetById(review.MovieId);
            if (movie == null)
            {
                throw new MovieApiException("Invalid movie id.");
            }

            _reviews.Add(review);
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

            return true;
        }

        public void Remove(int reviewId)
        {
            Review review = GetById(reviewId);
            _reviews.Remove(review);
        }
    }
}

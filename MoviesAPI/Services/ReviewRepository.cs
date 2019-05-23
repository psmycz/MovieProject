using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MovieAPIDbContext context;

        public ReviewRepository(MovieAPIDbContext context)
        {
            this.context = context;
        }

        public ReviewResponse Add(ReviewRequest reviewRequest)
        {
            Review review = new Review()
            {
                Comment = reviewRequest.Comment,
                Rate = reviewRequest.Rate,
            };

            var existingUsers = context.Users.Select(u => u.UserId).ToList();
            if (reviewRequest.UserId > 0 && existingUsers.Contains(reviewRequest.UserId))
            {
                review.UserId = reviewRequest.UserId;
            }
            else
                return null;

            var existingMovies = context.Movies.Select(m => m.Id).ToList();
            if (reviewRequest.MovieId > 0 && existingMovies.Contains(reviewRequest.MovieId))
            {
                review.MovieId = reviewRequest.MovieId;
            }
            else
                return null;

            context.Reviews.Add(review);
            context.SaveChanges();

            ReviewResponse reviewResponse = GetReview(review.Id);
            
            return reviewResponse;
        }

        public ReviewResponse Delete(int id)
        {
            Review review = context.Reviews.Find(id);
            if (review == null)
                return null;

            ReviewResponse reviewResponse = GetReview(id);

            context.Reviews.Remove(review);
            context.SaveChanges();

            return reviewResponse;
        }

        public IEnumerable<ReviewResponse> GetAllReviews()
        {
            IEnumerable<ReviewResponse> reviews = (from r in context.Reviews
                                                   select new ReviewResponse
                                                   {
                                                       Id = r.Id,
                                                       MovieTitle = r.Movie.Title,
                                                       Comment = r.Comment,
                                                       Rate = r.Rate,
                                                       User = (r.UserId > 0) ? r.User : null,
                                                   });
            return reviews;
        }

        public ReviewResponse GetReview(int id)
        {
            ReviewResponse review = (from r in context.Reviews
                                      where r.Id == id
                                      select new ReviewResponse
                                      {
                                          Id = r.Id,
                                          MovieTitle = r.Movie.Title,
                                          Comment = r.Comment,
                                          Rate = r.Rate,
                                          User = (r.UserId > 0) ? r.User : null,
                                      }).SingleOrDefault();
            return review;
        }

        public ReviewResponse Update(ReviewUpdateVM updatedReview)
        {
            var review = context.Reviews.Find(updatedReview.ReviewId);
            if (review == null)
                return null;

            review.Comment = updatedReview.Comment;
            review.Rate = updatedReview.Rate;
            context.SaveChanges();

            return GetReview(review.Id);
        }
    }
}

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

            var existingUsersIds = context.Users.Select(u => u.UserId).ToList();
            if (reviewRequest.UserId > 0 && existingUsersIds.Contains(reviewRequest.UserId))
            {
                review.UserId = reviewRequest.UserId;
                var user = context.Users.Find(reviewRequest.UserId);
                review.UserName = user.Name + ' ' + user.Surname;
            }
            else
                return null;

            var existingMoviesIds = context.Movies.Select(m => m.Id).ToList();
            if (reviewRequest.MovieId > 0 && existingMoviesIds.Contains(reviewRequest.MovieId))
            {
                review.MovieId = reviewRequest.MovieId;
            }
            else
                return null;

            context.Reviews.Add(review);
            context.SaveChanges();

            Movie movie = context.Movies.Find(review.MovieId);                // update field "userRating" for movie
            movie.UsersRating = movie.MovieReviews.Average(r => r.Rate);
            context.SaveChanges();

            ReviewResponse reviewResponse = GetReview(review.Id);
            
            return reviewResponse;
        }

        public ReviewResponse Delete(int id)
        {
            Review review = context.Reviews.Find(id);
            if (review == null)
                return null;

            Movie movie = context.Movies.Find(review.MovieId);                // update field "userRating" for movie
            ReviewResponse reviewResponse = GetReview(id);

            context.Reviews.Remove(review);
            context.SaveChanges();

            if (movie.MovieReviews.Any())
            {
                movie.UsersRating = movie.MovieReviews.Average(r => r.Rate);
                context.SaveChanges();
            }
            else
            {
                movie.UsersRating = null;
                context.SaveChanges();
            }                                                       // todo: does not update on user deletion, on delete cascade works different

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
                                                       Username = r.UserName
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
                                          Username = r.UserName
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

            Movie movie = context.Movies.Find(review.MovieId);                // update field "userRating" for movie
            movie.UsersRating = movie.MovieReviews.Average(r => r.Rate);
            context.SaveChanges();

            return GetReview(review.Id);
        }
    }
}

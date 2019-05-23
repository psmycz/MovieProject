using MoviesAPI.DbModels;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Interfaces 
{
    public interface IReviewRepository
    {
        ReviewResponse GetReview(int id);
        IEnumerable<ReviewResponse> GetAllReviews();
        ReviewResponse Add(ReviewRequest review);
        ReviewResponse Update(ReviewUpdateVM updatedReview);
        ReviewResponse Delete(int id);
    }
}

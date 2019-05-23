using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewRepository ReviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            ReviewRepository = reviewRepository;
        }
        #region GETALL
        /// <summary>
        /// Get all reviews
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                var reviews = ReviewRepository.GetAllReviews();

                if (reviews == null)
                    return NoContent();

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GET
        /// <summary>
        /// Get review by id
        /// </summary>
        /// <param name="reviewId">review id</param>
        /// <returns>Review if exist</returns>
        [HttpGet("{reviewId}")]
        public IActionResult Get(int reviewId)
        {
            try
            {
                var review = ReviewRepository.GetReview(reviewId);

                if (review == null)
                    return NoContent();

                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// Add new review
        /// </summary>
        /// <param name="review">Review</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]ReviewRequest review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newReview = ReviewRepository.Add(review);
                if (newReview == null)
                {
                    ModelState.AddModelError("","Movie or User does not exist.");
                    return BadRequest(ModelState);
                }

                return Created($"~/api/[controller]/{newReview.Id}", newReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        /// <summary>
        /// Update review in repositorium
        /// </summary>
        /// <param name="review">updated review</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]ReviewUpdateVM review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedReview = ReviewRepository.Update(review);

                if (updatedReview == null)
                    return NoContent();

                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete revie
        /// </summary>
        /// <param name="reviewId">review identifier</param>
        /// <returns></returns>
        [HttpDelete("{reviewId}")]
        public IActionResult Delete(int reviewId)
        {
            try
            {
                var deletedReview = ReviewRepository.Delete(reviewId);

                if (deletedReview == null)
                    return NoContent();

                return Ok(deletedReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}

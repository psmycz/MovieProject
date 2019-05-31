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
        private IReviewRepository reviewRepository;

        public ReviewController(IReviewRepository _reviewRepository)
        {
            reviewRepository = _reviewRepository;
        }

        /// <summary>
        /// Get all reviews
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                var reviews = reviewRepository.GetAllReviews();

                if (reviews == null)
                    return NoContent();

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



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
                var review = reviewRepository.GetReview(reviewId);

                if (review == null)
                    return NoContent();

                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



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
                var newReview = reviewRepository.Add(review);
                if (newReview == null)
                {
                    ModelState.AddModelError("","Movie or User does not exist.");
                    return BadRequest(ModelState);
                }

                return CreatedAtAction(nameof(Get), new { reviewId = newReview.Id }, newReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



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
                var updatedReview = reviewRepository.Update(review);

                if (updatedReview == null)
                    return NoContent();

                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



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
                var deletedReview = reviewRepository.Delete(reviewId);

                if (deletedReview == null)
                    return NoContent();

                return Ok(deletedReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

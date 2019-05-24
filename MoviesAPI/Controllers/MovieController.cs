using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;

namespace MoviesAPI.Controllers
{
    [EnableCors("*")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMovieRepository MovieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            MovieRepository = movieRepository;
        }

        #region GETALL 
        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>List of movies</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var movies = MovieRepository.GetAllMovies();

                if (movies == null)
                    return NoContent();

                return Ok(movies);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region  GET

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns>Movie if found</returns>
        [HttpGet("{movieId}")]
        public IActionResult Get(int movieId)
        {
            try
            {
                MovieResponse movie = MovieRepository.GetMovie(movieId);

                if (movie == null)
                    return NoContent();

                return Ok(movie);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region POST
        /// <summary>
        /// Add new movie to repositorium
        /// </summary>
        /// <param name="movie">new movie</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]MovieRequest movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newMovie = MovieRepository.Add(movie);

                return Created($"~/api/[controller]/{newMovie.Id}", newMovie);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        /// <summary>
        /// Update movie in repositorium
        /// </summary>
        /// <param name="movie">updated movie</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]MovieUpdateVM movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedMovie = MovieRepository.Update(movie);

                if (updatedMovie == null)
                    return NoContent();

                return Ok(updatedMovie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete movie from repositorium
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns></returns>
        [HttpDelete("{movieId}")]
        public IActionResult Delete(int movieId)
        {
            try
            {
                var deletedMovie = MovieRepository.Delete(movieId);
            
                if (deletedMovie == null)
                    return NoContent();

                return Ok(deletedMovie);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}

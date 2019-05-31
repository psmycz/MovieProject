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
        private IMovieRepository movieRepository;

        public MovieController(IMovieRepository _movieRepository)
        {
            movieRepository = _movieRepository;
        }


        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>List of movies</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var movies = movieRepository.GetAllMovies();

                if (movies == null)
                    return NoContent();

                return Ok(movies);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




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
                MovieResponse movie = movieRepository.GetMovie(movieId);

                if (movie == null)
                    return NoContent();

                return Ok(movie);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



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
                var newMovie = movieRepository.Add(movie);

                return CreatedAtAction(nameof(Get), new { movieId = newMovie.Id }, newMovie);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



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
                var updatedMovie = movieRepository.Update(movie);

                if (updatedMovie == null)
                    return NoContent();

                return Ok(updatedMovie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


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
                var deletedMovie = movieRepository.Delete(movieId);
            
                if (deletedMovie == null)
                    return NoContent();

                return Ok(deletedMovie);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

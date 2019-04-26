using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private IMoviesService _moviesService;

        public MovieController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>List of movies</returns>
        [HttpGet]
        public IActionResult GetAllMovies()
        {
            return Ok(_moviesService.GetAll());
        }

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns>Movie if found</returns>
        [HttpGet("{movieId}")]
        public IActionResult Get(int movieId)
        {
            Movie movie = _moviesService.GetById(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<MovieResponse>(movie));
        }

        /// <summary>
        /// Add new movie to repositorium
        /// </summary>
        /// <param name="movie">new movie</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]MovieRequest movie)
        {
            _moviesService.AddNewMovie(AutoMapper.Mapper.Map<Movie>(movie));

            return Ok();
        }

        /// <summary>
        /// Update movie in repositorium
        /// </summary>
        /// <param name="movie">updated movie</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]MovieRequest movie)
        {
            if (_moviesService.UpdateMovie(AutoMapper.Mapper.Map<Movie>(movie)))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete movie from repositorium
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns></returns>
        [HttpDelete("{movieId}")]
        public IActionResult Delete(int movieId)
        {
            _moviesService.Remove(movieId);
            return Ok();
        }
    }
}

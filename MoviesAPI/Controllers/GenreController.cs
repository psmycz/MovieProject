using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private IGenreRepository GenreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            GenreRepository = genreRepository;
        }

        #region GETALL
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                var genres = GenreRepository.GetAllGenres();

                if (genres == null)
                    return NoContent();

                return Ok(genres);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GET
        [HttpGet("{genreId}")]
        public IActionResult Get(int genreId)
        {
            try
            {
                var genre = GenreRepository.GetGenre(genreId);

                if (genre == null)
                    return NoContent();

                return Ok(genre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult Post([FromBody]GenreRequest genre)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newGenre = GenreRepository.Add(genre);
                if (newGenre == null)
                    return BadRequest();

                return Created($"~/api/[controller]/{newGenre.GenreId}", newGenre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        public IActionResult Put([FromBody]GenreUpdateVM genre)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedGenre = GenreRepository.Update(genre);

                if (updatedGenre == null)
                    return NoContent();

                return Ok(updatedGenre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("{genreId}")]
        public IActionResult Delete(int genreId)
        {
            try
            {
                var deletedGenre = GenreRepository.Delete(genreId);

                if (deletedGenre == null)
                    return NoContent();

                return Ok(deletedGenre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}

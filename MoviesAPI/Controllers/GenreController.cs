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
        private IGenreRepository genreRepository;

        public GenreController(IGenreRepository _genreRepository)
        {
            genreRepository = _genreRepository;
        }


        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                var genres = genreRepository.GetAllGenres();

                if (genres == null)
                    return NoContent();

                return Ok(genres);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{genreId}")]
        public IActionResult Get(int genreId)
        {
            try
            {
                var genre = genreRepository.GetGenre(genreId);

                if (genre == null)
                    return NoContent();

                return Ok(genre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public IActionResult Post([FromBody]GenreRequest genre)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newGenre = genreRepository.Add(genre);
                if (newGenre == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Get), new { genreId = newGenre.GenreId }, newGenre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        public IActionResult Put([FromBody]GenreUpdateVM genre)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedGenre = genreRepository.Update(genre);

                if (updatedGenre == null)
                    return NoContent();

                return Ok(updatedGenre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("{genreId}")]
        public IActionResult Delete(int genreId)
        {
            try
            {
                var deletedGenre = genreRepository.Delete(genreId);

                if (deletedGenre == null)
                    return NoContent();

                return Ok(deletedGenre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

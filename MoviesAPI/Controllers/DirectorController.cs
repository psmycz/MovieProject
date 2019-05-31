using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private IDirectorRepository directorRepository;

        public DirectorController(IDirectorRepository _directorRepository)
        {
            directorRepository = _directorRepository;
        }


        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                var directors = directorRepository.GetAllDirectors();

                if (directors == null)
                    return NoContent();

                return Ok(directors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{directorId}")]
        public IActionResult Get(int directorId)
        {
            try
            {
                var director = directorRepository.GetDirector(directorId);

                if (director == null)
                    return NoContent();

                return Ok(director);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]DirectorRequest director)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newDirector = directorRepository.Add(director);
                if (newDirector == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Get), new { directorId = newDirector.DirectorId}, newDirector);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]DirectorUpdateVM director)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedDirector = directorRepository.Update(director);

                if (updatedDirector == null)
                    return NoContent();

                return Ok(updatedDirector);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{directorId}")]
        public IActionResult Delete(int directorId)
        {
            try
            {
                var deletedDirector = directorRepository.Delete(directorId);

                if (deletedDirector == null)
                    return NoContent();

                return Ok(deletedDirector);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

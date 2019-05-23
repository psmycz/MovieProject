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
        private IDirectorRepository DirectorRepository;

        public DirectorController(IDirectorRepository directorRepository)
        {
            DirectorRepository = directorRepository;
        }

        #region GETALL
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                var directors = DirectorRepository.GetAllDirectors();

                if (directors == null)
                    return NoContent();

                return Ok(directors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GET
        [HttpGet("{directorId}")]
        public IActionResult Get(int directorId)
        {
            try
            {
                var director = DirectorRepository.GetDirector(directorId);

                if (director == null)
                    return NoContent();

                return Ok(director);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult Post([FromBody]DirectorRequest director)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newDirector = DirectorRepository.Add(director);
                if (newDirector == null)
                    return BadRequest();

                return Created($"~/api/[controller]/{newDirector.DirectorId}", newDirector);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        public IActionResult Put([FromBody]DirectorUpdateVM director)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedDirector = DirectorRepository.Update(director);

                if (updatedDirector == null)
                    return NoContent();

                return Ok(updatedDirector);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("{directorId}")]
        public IActionResult Delete(int directorId)
        {
            try
            {
                var deletedDirector = DirectorRepository.Delete(directorId);

                if (deletedDirector == null)
                    return NoContent();

                return Ok(deletedDirector);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}

using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository UserRepository;

        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        #region GETALL
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                var users = UserRepository.GetAllUsers();

                if (users == null)
                    return NoContent();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GET
        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            try
            {
                var user = UserRepository.GetUser(userId);

                if (user == null)
                    return NoContent();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult Post([FromBody]UserRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newUser = UserRepository.Add(user);
                if (newUser== null)
                    return BadRequest();

                return Created($"~/api/[controller]/{newUser.UserId}", newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        public IActionResult Put([FromBody]UserUpdateVM user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedUser = UserRepository.Update(user);

                if (updatedUser == null)
                    return NoContent();

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            try
            {
                var deletedUser = UserRepository.Delete(userId);

                if (deletedUser == null)
                    return NoContent();

                return Ok(deletedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}

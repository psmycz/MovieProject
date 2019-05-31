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
        private IUserRepository userRepository;

        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }


        [HttpGet]
        public IActionResult GetAllReviews()
        {
            try
            {
                var users = userRepository.GetAllUsers();

                if (users == null)
                    return NoContent();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            try
            {
                var user = userRepository.GetUser(userId);

                if (user == null)
                    return NoContent();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public IActionResult Post([FromBody]UserRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newUser = userRepository.Add(user);
                if (newUser== null)
                    return BadRequest();

                return CreatedAtAction(nameof(Get), new { userId = newUser.UserId }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut]
        public IActionResult Put([FromBody]UserUpdateVM user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedUser = userRepository.Update(user);

                if (updatedUser == null)
                    return NoContent();

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            try
            {
                var deletedUser = userRepository.Delete(userId);

                if (deletedUser == null)
                    return NoContent();

                return Ok(deletedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

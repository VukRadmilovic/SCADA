using BataSCADA.DTOs;
using BataSCADA.Services;
using BataSCADA.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BataSCADA.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : Controller {

        [HttpPost("login")]
        public IActionResult Login(LoginDTO userCredentials)
        {
            try
            {
                UserService.Login(userCredentials);
                return Ok(new SuccessMessageDTO("User successfully logged in!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Credentials",ex.Message));
            }
        }


        [HttpPost("logout/{username}")]
        public IActionResult Logout(string username)
        {
            try
            {
                UserService.Logout(username);
                return Ok(new SuccessMessageDTO("User successfully logged out!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Credentials", ex.Message));
            }
        }


        [HttpPost("register")]
        public IActionResult Register(UserDTO userInfo)
        {
            try
            {
                UserService.Register(userInfo);
                return Ok(new SuccessMessageDTO("User successfully registered!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Credentials", ex.Message));
            }
            
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(UserService.GetAllUsers());
        }
    }   
}

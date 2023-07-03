using BataSCADA.DTOs;
using BataSCADA.Models;
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
                return Ok("User successfully logged in!");
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
                return Ok("User successfully logged out!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Credentials", ex.Message));
            }
        }


        [HttpPost("register")]
        public IActionResult Register(UserDTO userInfo)
        {
            if (UserService.GetByUsername(userInfo.Username) != null)
            {
                return BadRequest(new GlobalError(400,"Username","Username already in use!"));
            }
            var user = new User(userInfo);
            UserService.Save(user);
            return Ok("User successfully registered!");
        }
    }   
}

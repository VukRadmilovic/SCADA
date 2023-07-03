using BataSCADA.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BataSCADA.Controllers
{
    [Route("tags")]
    public class TagController : Controller
    {
        [HttpPost("add")]
        
        public IActionResult AddTag()
        {
            return Ok();
        }
    }
}

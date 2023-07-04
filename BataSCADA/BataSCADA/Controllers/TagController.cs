using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BataSCADA.Controllers
{
    [Route("tags")]
    [EnableCors]
    public class TagController : Controller
    {
        [HttpPost("addInput")]
        
        public IActionResult AddInputTag()
        {
            return Ok();
        }
    }
}

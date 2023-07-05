using BataSCADA.DTOs;
using BataSCADA.Services;
using BataSCADA.Validation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BataSCADA.Controllers
{
    [Route("values")]
    [ApiController]
    [EnableCors]
    public class AddressValueController : Controller
    {
        [HttpPost("add-value")]
        public IActionResult AddAnalogInputTag(AddressValueDTO value)
        {
            try
            {
                AddressValueService.AddAddressValue(value);
                return Ok("Address value successfully added!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Address", ex.Message));
            }
        }
    }
}

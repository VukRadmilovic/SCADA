using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SCADA.DTOs;
using SCADA.Services;
using SCADA.Validation;

namespace SCADA.Controllers
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
                return Ok(new SuccessMessageDTO("Address value successfully added!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Address", ex.Message));
            }
        }
    }
}

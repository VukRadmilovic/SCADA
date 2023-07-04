using BataSCADA.DTOs;
using BataSCADA.Models;
using BataSCADA.Services;
using BataSCADA.Validation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BataSCADA.Controllers
{
    [Route("tags")]
    [ApiController]
    [EnableCors]
    public class TagController : Controller
    {
        [HttpPost("addAnalogInput")]
        public IActionResult AddAnalogInputTag(AnalogInputDTO tagInfo)
        {
            try
            {
                TagService.AddAnalogInput(tagInfo);
                return Ok("Analog input tag successfully added!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "TagName", ex.Message));
            }
        }

        [HttpPost("addAnalogOutput")]
        public IActionResult AddAnalogOutputTag(AnalogOutput tagInfo)
        {
            try
            {
                TagService.AddAnalogOutput(tagInfo);
                return Ok("Analog output tag successfully added!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "TagName", ex.Message));
            }
        }

        [HttpPost("addDigitalOutput")]
        public IActionResult AddDigitalOutputTag(DigitalOutput tagInfo)
        {
            try
            {
                TagService.AddDigitalOutput(tagInfo);
                return Ok("Digital output tag successfully added!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "TagName", ex.Message));
            }
        }

        [HttpPost("addDigitalInput")]
        public IActionResult AddDigitalInputTag(DigitalInput tagInfo)
        {
            try
            {
                TagService.AddDigitalInput(tagInfo);
                return Ok("Digital input tag successfully added!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "TagName", ex.Message));
            }
        }
    }
}

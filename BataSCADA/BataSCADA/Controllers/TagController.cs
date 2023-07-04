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
        [HttpPost("add-analog-input")]
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

        [HttpPost("add-analog-output")]
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

        [HttpPost("add-digital-output")]
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

        [HttpPost("add-digital-input")]
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

        [HttpDelete("delete/{tagName}")]
        public IActionResult DeleteTag(string tagName)
        {
            try
            {
                TagService.DeleteTag(tagName);
                return Ok("Tag successfully deleted!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "TagName", ex.Message));
            }
        }

        [HttpPut("turn-on-scan/{tagName}")]
        public IActionResult TurnOnScan(string tagName)
        {
            try
            {
                TagService.TurnOnTag(tagName);
                return Ok("Tag scan successfully turned on!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpPut("turn-off-scan/{tagName}")]
        public IActionResult TurnOffScan(string tagName)
        {
            try
            {
                TagService.TurnOffTag(tagName);
                return Ok("Tag scan successfully turned off!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

    }
}

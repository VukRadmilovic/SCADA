using SCADA.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using SCADA.Models.Enumerations;
using SCADA.DTOs;
using SCADA.Services;
using SCADA.Validation;

namespace SCADA.Controllers
{
    [Route("alarms")]
    [ApiController]
    [EnableCors]
    public class AlarmController : Controller
    {
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                AlarmService.Delete(id);
                return Ok(new SuccessMessageDTO("Alarm successfully deleted!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Alarm", ex.Message));
            }
        }

        [HttpGet("active")]
        public IActionResult GetActive()
        {
            return Ok(AlarmService.GetActive());
        }

        [HttpPut("snooze/{id}")]
        public IActionResult Snooze(int id)
        {
            try
            {
                AlarmService.Snooze(id);
                return Ok(new SuccessMessageDTO("Alarm successfully snoozed!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Snooze", ex.Message));
            }
        }
        [HttpGet("wpriority/{priority}")]
        public IActionResult WPriority(string priority)
        {
            try
            {
                priority = HttpUtility.UrlDecode(priority);
                return Ok(AlarmService.WPriority(int.Parse(priority)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }
        [HttpGet("some-time/{from}/{to}")]
        public IActionResult SomeTime(string from, string to)
        {
            try
            {
                from = HttpUtility.UrlDecode(from);
                to = HttpUtility.UrlDecode(to);
                DateTime FromTime = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(from));
                DateTime ToTime = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(to));
                return Ok(AlarmService.SomeTime(FromTime, ToTime));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }
    }
}

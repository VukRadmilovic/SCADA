using BataSCADA.DTOs;
using BataSCADA.Models;
using BataSCADA.Services;
using BataSCADA.Validation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using BataSCADA.Models.Enumerations;

namespace BataSCADA.Controllers
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
    }
}

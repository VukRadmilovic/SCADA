using BataSCADA.DTOs;
using BataSCADA.Models;
using BataSCADA.Services;
using BataSCADA.Validation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;

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
    }
}

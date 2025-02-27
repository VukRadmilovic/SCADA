﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using Microsoft.EntityFrameworkCore.Storage;
using SCADA.DTOs;
using SCADA.Models;
using SCADA.Services;
using SCADA.Validation;

namespace SCADA.Controllers
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
                return Ok(new SuccessMessageDTO("Analog input tag successfully added!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpPost("add-analog-output")]
        public IActionResult AddAnalogOutputTag(AnalogOutput tagInfo)
        {
            try
            {
                TagService.AddAnalogOutput(tagInfo);
                return Ok(new SuccessMessageDTO("Analog output tag successfully added!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpPost("add-digital-output")]
        public IActionResult AddDigitalOutputTag(DigitalOutput tagInfo)
        {
            try
            {
                TagService.AddDigitalOutput(tagInfo);
                return Ok(new SuccessMessageDTO("Digital output tag successfully added!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpPost("add-digital-input")]
        public IActionResult AddDigitalInputTag(DigitalInput tagInfo)
        {
            try
            {
                TagService.AddDigitalInput(tagInfo);
                return Ok(new SuccessMessageDTO("Digital input tag successfully added!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpDelete("delete/{tagName}")]
        public IActionResult DeleteTag(string tagName)
        {
            try
            {
                TagService.DeleteTag(tagName);
                return Ok(new SuccessMessageDTO("Tag successfully deleted!"));
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
                return Ok(new SuccessMessageDTO("Tag scan successfully turned on!"));
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
                return Ok(new SuccessMessageDTO("Tag scan successfully turned off!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpGet("output-tags")]
        public IActionResult GetAllOutputTags()
        {
            return Ok(TagService.GetAllOutputTags());
        }

        [HttpGet("input-tags")]
        public IActionResult GetAllInputTags()
        {
            return Ok(TagService.GetAllInputTags());
        }

        [HttpPut("output-tag-value-change/{tagName}")]
        public IActionResult ChangeOutputTagValue(string tagName, [FromBody] ValueDTO value)
        {
            try
            {
                tagName = HttpUtility.UrlDecode(tagName);
                TagService.ChangeOutputTagValue(tagName, value.Value);
                return Ok(new SuccessMessageDTO("Output tag value successfully changed!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpGet("scan-input-tag/{tagName}")]
        public IActionResult ScanInputTag(string tagName)
        {
            try
            {
                tagName = HttpUtility.UrlDecode(tagName);
                double value = TagService.ScanInputTag(tagName);
                return Ok(value);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpPost("add-alarm/{tagName}")]
        public IActionResult AddAlarm(Alarm alarm, string tagName)
        {
            try
            {
                tagName = HttpUtility.UrlDecode(tagName);
                TagService.AddAlarm(alarm, tagName);
                return Ok(new SuccessMessageDTO("Alarm has been successfully added!"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpGet("tag-last-value/{tagName}")]
        public IActionResult TagLastValue(string tagName)
        {
            try
            {
                tagName = HttpUtility.UrlDecode(tagName);
                double value = TagService.TagLastValue(tagName);
                return Ok(value);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpGet("tag-all-values/{tagName}")]
        public IActionResult TagAllValues(string tagName)
        {
            try
            {
                tagName = HttpUtility.UrlDecode(tagName);
                return Ok(TagService.TagAllValues(tagName));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }

        [HttpGet("digital-all-last")]
        public IActionResult DigitalAllLast()
        {
            return Ok(TagService.DigitalLast());
        }

        [HttpGet("analog-all-last")]
        public IActionResult DigitalAllValues()
        {
            return Ok(TagService.AnalogLast());
        }

        [HttpGet("all-time/{timeFrom}/{timeTo}")]
        public IActionResult AllTime(string timeFrom, string timeTo)
        {
            try
            {
                timeFrom = HttpUtility.UrlDecode(timeFrom);
                timeTo = HttpUtility.UrlDecode(timeTo);
                DateTime from = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(timeFrom));
                DateTime to = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(timeTo));
                return Ok(TagService.AllTime(from, to));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new GlobalError(400, "Tag", ex.Message));
            }
        }
    }
}

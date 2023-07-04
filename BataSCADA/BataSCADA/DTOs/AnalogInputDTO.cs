using System.ComponentModel.DataAnnotations;
using BataSCADA.Models.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BataSCADA.DTOs
{
    public class AnalogInputDTO
    {
        [Required(ErrorMessage = "TagName is a required field!")]
        public string? TagName { get; set; }
        [Required(ErrorMessage = "Description is a required field!")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Address is a required field!")]
        [Range(0, int.MaxValue, ErrorMessage = "Address has to be a value of 0 or more!")]
        public int Address { get; set; }
        [Required(ErrorMessage = "Driver is a required field!")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DriverType Driver { get; set; }
        [Required(ErrorMessage = "ScanTime is a required field!")]
        [Range(0, int.MaxValue, ErrorMessage = "ScanTime has to be a value of 0 or more!")]
        public int ScanTime { get; set; }
        [Required(ErrorMessage = "IsOn is a required field!")]
        public bool IsOn{ get; set; }
        [Required(ErrorMessage = "LowLimit is a required field!")]
        public double LowLimit { get; set; }
        [Required(ErrorMessage = "HighLimit is a required field!")]
        public double HighLimit { get; set; }
        [Required(ErrorMessage = "Units is a required field!")]
        public string? Units { get; set; }
    }
}

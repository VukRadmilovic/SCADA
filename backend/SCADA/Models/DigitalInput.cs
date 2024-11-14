using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SCADA.Models.Enumerations;

namespace SCADA.Models
{
    public class DigitalInput : Tag
    {
        [Required(ErrorMessage = "Driver is a required field!")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DriverType Driver { get; set; }
        [Required(ErrorMessage = "ScanTime is a required field!")]
        [Range(0, int.MaxValue, ErrorMessage = "ScanTime has to be a value of 0 or more!")]
        public int ScanTime { get; set; }
        [Required(ErrorMessage = "IsOn is a required field!")]
        public bool IsOn { get; set; }

        public DigitalInput() {}

    }
}

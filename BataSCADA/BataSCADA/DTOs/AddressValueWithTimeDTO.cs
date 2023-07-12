using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BataSCADA.DTOs
{
    public class AddressValueWithTimeDTO
    {
        [Required(ErrorMessage = "Address is a required field!")]
        public int Address { get; set; }

        [Required(ErrorMessage = "Value is a required field!")]
        public double Value { get; set; }

        [Required(ErrorMessage = "ValueType is a required field!")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Models.Enumerations.ValueType ValueType { get; set; }

        [Required(ErrorMessage = "Time is a required field!")]
        public DateTime Timestamp { get; set; }
    }
}

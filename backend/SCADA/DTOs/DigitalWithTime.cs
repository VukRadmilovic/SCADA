using System.ComponentModel.DataAnnotations;

namespace SCADA.DTOs
{
    public class DigitalWithTime
    {
        [Required(ErrorMessage = "TagName is a required field!")]
        public string TagName { get; set; }

        [Required(ErrorMessage = "Address is a required field!")]
        public int Address { get; set; }

        [Required(ErrorMessage = "Value is a required field!")]
        public double Value { get; set; }

        [Required(ErrorMessage = "Time is a required field!")]
        public DateTime Timestamp { get; set; }
    }
}
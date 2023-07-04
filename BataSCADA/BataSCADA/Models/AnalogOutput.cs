using System.ComponentModel.DataAnnotations;

namespace BataSCADA.Models
{
    public class AnalogOutput :  Tag
    {
        [Required(ErrorMessage = "InitialValue is a required field!")]
        public double InitialValue { get; set; }
        [Required(ErrorMessage = "LowLimit is a required field!")]
        public double LowLimit { get; set; }
        [Required(ErrorMessage = "HighLimit is a required field!")]
        public double HighLimit { get; set; }
        [Required(ErrorMessage = "Units is a required field!")]
        public string Units { get; set; }

        public AnalogOutput() { }
    }
}

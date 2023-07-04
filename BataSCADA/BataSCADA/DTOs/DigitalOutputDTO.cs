using System.ComponentModel.DataAnnotations;

namespace BataSCADA.DTOs
{
    public class DigitalOutputDTO
    {
        [Required(ErrorMessage = "TagName is a required field!")]
        public string? TagName { get; set; }
        [Required(ErrorMessage = "Description is a required field!")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Address is a required field!")]
        [Range(0, int.MaxValue, ErrorMessage = "Address has to be a value of 0 or more!")]
        public int Address { get; set; }
        [Required(ErrorMessage = "InitialValue is a required field!")]
        public double InitialValue { get; set; }
    }
}

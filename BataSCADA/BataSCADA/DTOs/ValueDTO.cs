using System.ComponentModel.DataAnnotations;

namespace BataSCADA.DTOs
{
    public class ValueDTO
    {
        [Required(ErrorMessage = "Value is a required field!")]
        public double Value { get; set; }
    }
}

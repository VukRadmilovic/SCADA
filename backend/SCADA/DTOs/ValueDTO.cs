using System.ComponentModel.DataAnnotations;

namespace SCADA.DTOs
{
    public class ValueDTO
    {
        [Required(ErrorMessage = "Value is a required field!")]
        public double Value { get; set; }
    }
}

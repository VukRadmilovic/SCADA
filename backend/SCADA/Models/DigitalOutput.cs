using SCADA.DTOs;
using System.ComponentModel.DataAnnotations;

namespace SCADA.Models
{
    public class DigitalOutput : Tag
    {
        [Required(ErrorMessage = "InitialValue is a required field!")]
        public bool InitialValue { get; set; }
        public bool Value { get; set; }

        public DigitalOutput() {}

    }
}

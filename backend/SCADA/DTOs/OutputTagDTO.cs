using SCADA.Models;
using SCADA.Models.Enumerations;

namespace SCADA.DTOs
{
    public class OutputTagDTO
    {
        public string TagName { get; set; }
        public string Description { get; set; }
        public int Address { get; set; }
        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }
        public TagType Type { get; set; }
        public double Value { get; set; }

        public OutputTagDTO(DigitalOutput digitalOutput)
        {
            TagName = digitalOutput.TagName;
            Description = digitalOutput.Description;
            Address = digitalOutput.Address;
            InitialValue = digitalOutput.InitialValue ? 1 : 0;
            LowLimit = 0;
            HighLimit = 0;
            Units = "";
            Type = TagType.Digital;
            Value = digitalOutput.Value ? 1 : 0;
        }

        public OutputTagDTO(AnalogOutput analogOutput)
        {
            TagName = analogOutput.TagName;
            Description = analogOutput.Description;
            Address = analogOutput.Address;
            InitialValue = analogOutput.InitialValue;
            LowLimit = analogOutput.LowLimit;
            HighLimit = analogOutput.HighLimit;
            Units = analogOutput.Units;
            Type = TagType.Analog;
            Value = analogOutput.Value;
        }
    }
}

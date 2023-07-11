using BataSCADA.Models;
using BataSCADA.Models.Enumerations;

namespace BataSCADA.DTOs
{
    public class InputTagDTO
    {
        public string TagName { get; set; }
        public string Description { get; set; }
        public int Address { get; set; }
        public DriverType Driver { get; set; }
        public  int ScanTime { get; set; }
        public bool IsOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }
        public double Value { get; set; }
        public TagType Type { get; set; }


        public InputTagDTO(DigitalInput digitalInput)
        {
            TagName = digitalInput.TagName;
            Description = digitalInput.Description;
            Address = digitalInput.Address;
            Driver = digitalInput.Driver;
            ScanTime = digitalInput.ScanTime;
            IsOn = digitalInput.IsOn;
            LowLimit = 0;
            HighLimit = 0;
            Units = "";
            Type = TagType.Digital;
        }

        public InputTagDTO(AnalogInput analogInput)
        {
            TagName = analogInput.TagName;
            Description = analogInput.Description;
            Address = analogInput.Address;
            Driver = analogInput.Driver;
            ScanTime = analogInput.ScanTime;
            IsOn = analogInput.IsOn;
            LowLimit = analogInput.LowLimit;
            HighLimit = analogInput.HighLimit;
            Units = analogInput.Units;
            Type = TagType.Analog;
        }

    }
}

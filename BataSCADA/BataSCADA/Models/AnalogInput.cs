using BataSCADA.DTOs;
using BataSCADA.Models.Enumerations;

namespace BataSCADA.Models
{
    public class AnalogInput : Tag
    {
        public DriverType Driver { get; set; }
        public int ScanTime { get; set; }
        public bool IsOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }

        public AnalogInput() {}

        public AnalogInput(AnalogInputDTO tagInfo)
        {
            TagName = tagInfo.TagName;
            Description = tagInfo.Description;
            Address = tagInfo.Address;
            Driver = tagInfo.Driver;
            ScanTime = tagInfo.ScanTime;
            IsOn = tagInfo.IsOn;
            LowLimit = tagInfo.LowLimit;
            HighLimit = tagInfo.HighLimit;
            Units = tagInfo.Units;
        }
    }
}

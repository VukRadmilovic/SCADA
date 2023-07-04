using BataSCADA.Models.Enumerations;

namespace BataSCADA.Models
{
    public class AnalogInput : Tag
    {
        public DriverType Driver { get; set; }
        public int ScanTime { get; set; }
        public bool IsOn { get; set; }
        public List<Alarm> Alarms { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }

        public AnalogInput()
        {
            Driver = DriverType.None;
            ScanTime = 0;
            IsOn = false;
            Alarms = new List<Alarm>();
            LowLimit = 0;
            HighLimit = 0;
            Units = "";
        }
    }
}

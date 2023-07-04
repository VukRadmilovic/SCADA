using BataSCADA.Models.Enumerations;

namespace BataSCADA.Models
{
    public class DigitalInput : Tag
    {
        public DriverType Driver { get; set; }
        public int ScanTime { get; set; }
        public bool IsOn { get; set; }

        public DigitalInput()
        {
            Driver = DriverType.None;
            ScanTime = 0;
            IsOn = false;
        }
    }
}

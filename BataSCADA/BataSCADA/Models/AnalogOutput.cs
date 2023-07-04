namespace BataSCADA.Models
{
    public class AnalogOutput :  Tag
    {
        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }

        public AnalogOutput()
        {
            InitialValue = 0;
            LowLimit = 0;
            HighLimit = 0;
            Units = "";
        }
    }
}

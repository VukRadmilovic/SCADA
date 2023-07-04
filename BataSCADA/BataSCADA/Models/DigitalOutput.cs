namespace BataSCADA.Models
{
    public class DigitalOutput : Tag
    {
        public bool InitialValue { get; set; }

        public DigitalOutput()
        {
            InitialValue = false;
        }
    }
}

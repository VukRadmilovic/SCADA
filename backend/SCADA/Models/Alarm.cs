using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SCADA.Models.Enumerations;

namespace SCADA.Models
{
    public class Alarm
    {
        [Key]
        public int Id { get; set; }
        public AlarmType Type { get; set; }
        public int Priority { get; set; }
        public double Limit { get; set; }
        public string AnalogInputTagName { get; set; }
        public bool Triggered { get; set; }
        public DateTime? SnoozedUntil { get; set; }

        public Alarm()
        {
            Type = AlarmType.Low;
            Priority = 0;
            Limit = 0;
            Triggered = false;
            SnoozedUntil = null;
        }

        public Alarm(AlarmType type, int priority, double limit)
        {
            Type = type;
            Priority = priority;
            Limit = limit;
            Triggered = false;
            SnoozedUntil = null;
        }
    }
}

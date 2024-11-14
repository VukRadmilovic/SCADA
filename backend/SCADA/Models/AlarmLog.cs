using System.ComponentModel.DataAnnotations;

namespace SCADA.Models
{
    public class AlarmLog
    {
        [Key]
        public int Id { get; set; }
        public string TagName { get; set; }
        public int AlarmId { get; set; }
        public double Value { get; set; }
        public double Limit { get; set; }
        public bool IsTriggered { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using BataSCADA.Models.Enumerations;

namespace BataSCADA.Models
{
    public class Alarm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public AlarmType Type { get; set; }
        public int Priority { get; set; }
        public double Limit { get; set; }

        public Alarm()
        {
            Type = AlarmType.Low;
            Priority = 0;
            Limit = 0;
        }
    }
}

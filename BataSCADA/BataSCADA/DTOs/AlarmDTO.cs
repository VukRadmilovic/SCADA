using BataSCADA.Models;
using BataSCADA.Models.Enumerations;
using BataSCADA.Repositories;

namespace BataSCADA.DTOs
{
    public class AlarmDTO
    {
        public int Id { get; set; }
        public AlarmType Type { get; set; }
        public int Priority { get; set; }
        public double Limit { get; set; }
        public string TagName { get; set; }

        public AlarmDTO() { }

        public AlarmDTO(Alarm alarm)
        {
            Id = alarm.Id;
            Type = alarm.Type;
            Priority = alarm.Priority;
            Limit = alarm.Limit;
            TagName = alarm.AnalogInputTagName;
        }
    }
}

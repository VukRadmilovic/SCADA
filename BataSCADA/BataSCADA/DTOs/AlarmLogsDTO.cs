using BataSCADA.Models;
using BataSCADA.Models.Enumerations;
using BataSCADA.Repositories;

namespace BataSCADA.DTOs
{
    public class AlarmLogsDTO
    {
        public int Id { get; set; }
        public AlarmType Type { get; set; }
        public int Priority { get; set; }
        public double Limit { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

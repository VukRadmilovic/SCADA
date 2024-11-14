using SCADA.Models;
using SCADA.Repositories;
using SCADA.Models.Enumerations;

namespace SCADA.DTOs
{
    public class AlarmLogsDTO
    {
        public int Id { get; set; }
        public AlarmType Type { get; set; }
        public int Priority { get; set; }
        public string TagName { get; set; }
        public double Limit { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

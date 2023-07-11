using BataSCADA.Models;
using BataSCADA.Repositories;
using BataSCADA.Utils;

namespace BataSCADA.Services
{
    public class AlarmService
    {
        public static List<Alarm>? GetByTagName(string tagName)
        {
            return AlarmRepository.GetByTagName(tagName);
        }
    }
}

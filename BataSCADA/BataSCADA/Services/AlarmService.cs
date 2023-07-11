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

        internal static void Delete(int id)
        {
            if (AlarmRepository.GetById(id) == null)
                throw new ArgumentException("Alarm with the specified id does not exist!");
            AlarmRepository.Delete(id);
        }
    }
}

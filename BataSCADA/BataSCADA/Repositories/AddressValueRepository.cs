using BataSCADA.Models;
using BataSCADA.Utils;

namespace BataSCADA.Repositories
{
    public class AddressValueRepository
    {
        public static void SaveValue(AddressValue value)
        {
            using var dbContext = new DatabaseContext();
            dbContext.AddressValues.Add(value);
            dbContext.SaveChanges();
        }

        public static AddressValue GetLastValueByAddress(int address)
        {
            using var dbContext = new DatabaseContext();
            var value = dbContext.AddressValues.OrderByDescending(value => value.Timestamp)
                .FirstOrDefault(value => value.Address == address);
            return value;
        }
    }
}

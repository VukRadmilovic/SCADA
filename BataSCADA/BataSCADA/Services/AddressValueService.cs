using BataSCADA.DTOs;
using BataSCADA.Models;
using BataSCADA.Repositories;
using ValueType = BataSCADA.Models.Enumerations.ValueType;

namespace BataSCADA.Services
{
    public class AddressValueService
    {
        public static void AddAddressValue(AddressValueDTO value)
        {
            if (value.Address is < 1 or > 17)
            {
                throw new ArgumentException("Address value is not valid!");
            }

            if (value.ValueType == ValueType.Digital)
            {
                if (value.Value <= 0) value.Value = 0;
                else value.Value = 1;
            }
            var fullAddressValue = new AddressValue(value);
            AddressValueRepository.SaveValue(fullAddressValue);
        }
    }
}

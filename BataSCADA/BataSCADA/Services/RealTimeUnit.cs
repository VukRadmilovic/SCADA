using Microsoft.Extensions.Hosting;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using BataSCADA.DTOs;
using BataSCADA.Models;
using BataSCADA.Repositories;
using ValueType = BataSCADA.Models.Enumerations.ValueType;

namespace BataSCADA.Services
{
    public class RealTimeUnit : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var inputTags = TagRepository.GetAllInputTags();

                RandomNumberGenerator.Create();

                foreach (var inputTag in inputTags)
                {
                    if (inputTag.Address > 17)
                    {
                        continue;
                    }

                    int val = -1;
                    ValueType type = ValueType.Analog;

                    if (inputTag is AnalogInput analogInput)
                    {
                        val = RandomNumberGenerator.GetInt32((int)Math.Ceiling(analogInput.LowLimit), (int)Math.Floor(analogInput.HighLimit) + 1);
                    }
                    else if (inputTag is DigitalInput)
                    {
                        val = RandomNumberGenerator.GetInt32(0, 2);
                        type = ValueType.Digital;
                    }

                    AddressValueDTO dto = new AddressValueDTO()
                    {
                        Address = inputTag.Address,
                        Value = val,
                        ValueType = type
                    };

                    try
                    {
                        if (inputTag is AnalogInput analogInput2)
                        {
                            AlarmService.TriggerIfNeeded(analogInput2, dto.Value);
                        }
                        AddressValueService.AddAddressValue(dto);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}

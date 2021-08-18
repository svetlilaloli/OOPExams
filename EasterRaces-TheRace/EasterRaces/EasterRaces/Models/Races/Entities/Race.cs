using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private string name;
        private int laps;
        private readonly List<IDriver> drivers;
        private const int MinNameLength = 5;
        private const int MinLaps = 1;
        public Race(string name, int laps)
        {
            Name = name;
            Laps = laps;
            drivers = new List<IDriver>();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < MinNameLength)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, MinNameLength));
                }
                name = value;
            }
        }

        public int Laps
        {
            get => laps;
            private set
            {
                if (value < MinLaps)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidNumberOfLaps, MinLaps));
                }
                laps = value;
            }
        }

        public IReadOnlyCollection<IDriver> Drivers => drivers.AsReadOnly();

        public void AddDriver(IDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(ExceptionMessages.DriverInvalid);
            }
            if (!driver.CanParticipate)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriverNotParticipate, driver.Name));
            }
            if (drivers.Any(x => x.Name == driver.Name))
            {
                throw new ArgumentNullException(string.Format(ExceptionMessages.DriverAlreadyAdded, driver.Name, Name));
            }
            drivers.Add(driver);
        }
    }
}

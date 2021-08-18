using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private string name;
        private const int MinNameLength = 5;
        public Driver(string name)
        {
            Name = name;
            NumberOfWins = 0;
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

        public ICar Car { get; private set; }

        public int NumberOfWins { get; private set; }

        public bool CanParticipate
        {
            get
            {
                if (Car == null)
                {
                    return false;
                }
                return true;
            }
        }

        public void AddCar(ICar car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(ExceptionMessages.CarInvalid);
            }
            Car = car;
        }

        public void WinRace()
        {
            NumberOfWins++;
        }
    }
}

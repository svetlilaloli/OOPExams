using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Cars.Entities
{
    public abstract class Car : ICar
    {
        private string model;
        private int horsePower;
        private readonly int minHorsePower;
        private readonly int maxHorsePower;
        private const int MinModelLength = 4;
        public Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            Model = model;
            CubicCentimeters = cubicCentimeters;
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;
            HorsePower = horsePower;
        }
        public string Model 
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < MinModelLength)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidModel, value, MinModelLength));
                }
                model = value;
            }
        }

        public int HorsePower 
        {
            get => horsePower;
            private set
            {
                if (value < minHorsePower || value > maxHorsePower)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, value));
                }
                horsePower = value;
            }
        }

        public double CubicCentimeters { get; }

        public double CalculateRacePoints(int laps)
        {
            return CubicCentimeters / HorsePower * laps;
        }
    }
}

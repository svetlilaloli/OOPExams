namespace CarRacing.Core
{
    using CarRacing.Core.Contracts;
    using CarRacing.Models.Cars.Contracts;
    using CarRacing.Models.Cars.Entities;
    using CarRacing.Models.Maps.Contracts;
    using CarRacing.Models.Maps.Entities;
    using CarRacing.Models.Racers.Contracts;
    using CarRacing.Models.Racers.Entities;
    using CarRacing.Repositories.Entities;
    using CarRacing.Utilities.Messages;
    using System;
    using System.Linq;
    using System.Text;
    public class Controller : IController
    {
        private readonly CarRepository cars;
        private readonly RacerRepository racers;
        private readonly IMap map;
        private const string SuperCar = "SuperCar";
        private const string TunedCar = "TunedCar";
        private const string ProfessionalRacer = "ProfessionalRacer";
        private const string StreetRacer = "StreetRacer";
        public Controller()
        {
            cars = new CarRepository();
            racers = new RacerRepository();
            map = new Map();
        }
        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar car;
            if (type == SuperCar)
            {
                car = new SuperCar(make, model, VIN, horsePower);
            }
            else if (type == TunedCar)
            {
                car = new TunedCar(make, model, VIN, horsePower);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }
            cars.Add(car);

            return string.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            IRacer racer;
            ICar car = cars.FindBy(carVIN);

            if (car == null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }

            if (type == ProfessionalRacer)
            {
                racer = new ProfessionalRacer(username, car);
            }
            else if (type == StreetRacer)
            {
                racer = new StreetRacer(username, car);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidRacerType);
            }
            racers.Add(racer);

            return string.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racerOne = racers.FindBy(racerOneUsername);
            IRacer racerTwo = racers.FindBy(racerTwoUsername);

            if (racerOne == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            }
            if (racerTwo == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            }

            return map.StartRace(racerOne, racerTwo);
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();

            foreach (var racer in racers.Models.OrderByDescending(x => x.DrivingExperience).ThenBy(x => x.Username))
            {
                result.AppendLine($"{racer.GetType().Name}: {racer.Username}");
                result.AppendLine($"--Driving behavior: {racer.RacingBehavior}");
                result.AppendLine($"--Driving experience: {racer.DrivingExperience}");
                result.AppendLine($"--Car: {racer.Car.Make} {racer.Car.Model} ({racer.Car.VIN})");
            }
            return result.ToString().Trim();
        }
    }
}

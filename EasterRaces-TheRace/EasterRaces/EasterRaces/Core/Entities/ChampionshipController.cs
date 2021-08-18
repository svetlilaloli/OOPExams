using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private readonly CarRepository carRepository;
        private readonly DriverRepository driverRepository;
        private readonly RaceRepository raceRepository;
        private const int MinRaceParticipants = 3;
        public ChampionshipController()
        {
            carRepository = new CarRepository();
            driverRepository = new DriverRepository();
            raceRepository = new RaceRepository();
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            var driver = driverRepository.GetByName(driverName);
            var car = carRepository.GetByName(carModel);

            if (driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            if (car == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }
            driver.AddCar(car);
            return string.Format(OutputMessages.CarAdded, driver.Name, car.Model);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            var race = raceRepository.GetByName(raceName);
            var driver = driverRepository.GetByName(driverName);
            if (race == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            if (driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            race.AddDriver(driver);
            return string.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if (carRepository.GetByName(model) != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
            }
            ICar car;
            if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }
            else
            {
                car = new SportsCar(model, horsePower);
            }
            carRepository.Add(car);
            return string.Format(OutputMessages.CarCreated, car.GetType().Name, model);
        }

        public string CreateDriver(string driverName)
        {
            if (driverRepository.GetByName(driverName) != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }
            Driver driver = new Driver(driverName);
            driverRepository.Add(driver);
            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateRace(string name, int laps)
        {
            if (raceRepository.GetByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }
            var race = new Race(name, laps);
            raceRepository.Add(race);
            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string StartRace(string raceName)
        {
            var race = raceRepository.GetByName(raceName);
            var result = new StringBuilder();

            if (race == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            if (race.Drivers.Count < MinRaceParticipants)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, MinRaceParticipants));
            }
            var sorted = race.Drivers.OrderByDescending(x => x.Car.CalculateRacePoints(race.Laps)).ToList();

            result.Append($"Driver {sorted[0].Name} wins {raceName} race.");
            result.Append($"\nDriver {sorted[1].Name} is second in {raceName} race.");
            result.Append($"\nDriver {sorted[2].Name} is third in {race.Name} race.");
            
            return result.ToString();
        }
        public void End()
        {
            Environment.Exit(0);
        }
    }
}

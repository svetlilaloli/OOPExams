using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private readonly CarRepository carRepository;
        private readonly DriverRepository driverRepository;
        private readonly RaceRepository raceRepository;
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
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }
            if (car == null)
            {
                throw new InvalidOperationException($"Car {carModel} could not be found.");
            }
            driver.AddCar(car);
            return $"Driver {driver.Name} received car {car.Model}.";
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            var race = raceRepository.GetByName(raceName);
            var driver = driverRepository.GetByName(driverName);
            if (race == null)
            {
                throw new InvalidOperationException($"Race { raceName } could not be found.");
            }
            if (driver == null)
            {
                throw new InvalidOperationException($"Driver { driverName } could not be found.");
            }
            race.AddDriver(driver);
            return $"Driver {driverName} added in {raceName} race.";
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if (carRepository.GetByName(model) != null)
            {
                throw new ArgumentException($"Car {model} is already created.");
            }
            if (type == "Muscle")
            {
                var car = new MuscleCar(model, horsePower);
                carRepository.Add(car);
                return $"MuscleCar {model} is created.";
            }
            else
            {
                var car = new SportsCar(model, horsePower);
                carRepository.Add(car);
                return $"SportsCar {model} is created.";
            }
        }

        public string CreateDriver(string driverName)
        {
            if (driverRepository.GetByName(driverName) != null)
            {
                throw new ArgumentException($"Driver {driverName} is already created.");
            }
            Driver driver = new Driver(driverName);
            driverRepository.Add(driver);
            return $"Driver {driverName} is created.";
        }

        public string CreateRace(string name, int laps)
        {
            if (raceRepository.GetByName(name) != null)
            {
                throw new InvalidOperationException($"Race { name } is already create.");
            }
            var race = new Race(name, laps);
            raceRepository.Add(race);
            return $"Race {name} is created.";
        }

        public string StartRace(string raceName)
        {
            var race = raceRepository.GetByName(raceName);
            var result = new StringBuilder();

            if (race == null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }
            if (race.Drivers.Count < 3)
            {
                throw new InvalidOperationException($"Race {raceName} cannot start with less than 3 participants.");
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

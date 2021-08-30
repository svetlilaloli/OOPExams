using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private readonly AstronautRepository astronauts;
        private readonly PlanetRepository planets;
        private const string Biologist = "Biologist";
        private const string Geodesist = "Geodesist";
        private const string Meteorologist = "Meteorologist";
        private int exploredPlanetsCount;
        public Controller()
        {
            astronauts = new AstronautRepository();
            planets = new PlanetRepository();
            exploredPlanetsCount = 0;
        }
        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut;
            switch (type)
            {
                case Biologist: astronaut = new Biologist(astronautName); break;
                case Geodesist: astronaut = new Geodesist(astronautName); break;
                case Meteorologist:astronaut = new Meteorologist(astronautName); break;
                default: throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }
            astronauts.Add(astronaut);

            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            Planet planet = new Planet(planetName);
            planet.Items = items;
            planets.Add(planet);

            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var planet = planets.FindByName(planetName);
            var suitableAstronauts = astronauts.Models.Where(x => x.Oxygen > 60).ToList();

            if (suitableAstronauts.Count() == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            IMission mission = new Mission();
            mission.Explore(planet, suitableAstronauts);
            exploredPlanetsCount++;
            var deadAstronautsCount = suitableAstronauts.Count(x => !x.CanBreath);

            return string.Format(OutputMessages.PlanetExplored, planetName, deadAstronautsCount);
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"{exploredPlanetsCount} planets were explored!");
            result.AppendLine("Astronauts info:");

            foreach (var astronaut in astronauts.Models)
            {
                result.AppendLine($"Name: {astronaut.Name}");
                result.AppendLine($"Oxygen: {astronaut.Oxygen}");
                if (astronaut.Bag.Items.Count == 0)
                {
                    result.AppendLine($"Bag items: none");
                }
                else
                {
                    result.AppendLine($"Bag items: {string.Join(", ", astronaut.Bag.Items)}");
                }
            }

            return result.ToString().Trim();
        }

        public string RetireAstronaut(string astronautName)
        {
            var astronaut = astronauts.FindByName(astronautName);
            if (astronaut == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }
            astronauts.Remove(astronaut);

            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}

using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System.Collections.Generic;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach (var astronaut in astronauts)
            {
                if (astronaut.CanBreath)
                {
                    foreach (var item in planet.Items)
                    {
                        while (astronaut.CanBreath)
                        {
                            astronaut.Breath();
                            astronaut.Bag.Items.Add(item);
                            planet.Items.Remove(item);
                        }
                        break;
                    }
                }
            }
        }
    }
}

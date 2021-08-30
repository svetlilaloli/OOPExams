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
            var items = new List<string>(planet.Items);
            var itemsLeft = new List<string>(items);

            foreach (var astronaut in astronauts)
            {
                if (itemsLeft.Count == 0)
                {
                    break;
                }
                foreach (var item in items)
                {
                    if (astronaut.CanBreath)
                    {
                        astronaut.Breath();
                        astronaut.Bag.Items.Add(item);
                        itemsLeft.Remove(item);
                    }
                    else
                    {
                        items = new List<string>(itemsLeft);
                        break;
                    }
                }
            }
        }
    }
}

using SpaceStation.Models.Astronauts.Contracts;

namespace SpaceStation.Models.Astronauts
{
    public class Geodesist : Astronaut, IAstronaut
    {
        private const double InitialOxygen = 50;
        public Geodesist(string name) : base (name, InitialOxygen)
        {
        }
    }
}

using SpaceStation.Models.Astronauts.Contracts;

namespace SpaceStation.Models.Astronauts
{
    public class Meteorologist : Astronaut, IAstronaut
    {
        private const double InitialOxygen = 90;
        public Meteorologist(string name) : base (name, InitialOxygen)
        {
        }
    }
}

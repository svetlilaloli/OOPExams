using SpaceStation.Models.Astronauts.Contracts;

namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut, IAstronaut
    {
        private const double InitialOxygen = 70;
        public Biologist(string name) : base (name, InitialOxygen)
        {
        }
        public override void Breath()
        {
            Oxygen -= 5;
        }
    }
}

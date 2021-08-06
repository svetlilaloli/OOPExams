using Easter.Models.Bunnies.Contracts;

namespace Easter.Models.Bunnies
{
    public abstract class SleepyBunny : Bunny, IBunny
    {
        private const int energy = 50;
        public SleepyBunny(string name) : base(name, energy)
        {
        }
        public new void Work()
        {
            base.Work();
            Energy -= 5;
        }
    }
}

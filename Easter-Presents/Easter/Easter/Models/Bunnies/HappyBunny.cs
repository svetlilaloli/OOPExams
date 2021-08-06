using Easter.Models.Bunnies.Contracts;

namespace Easter.Models.Bunnies
{
    public class HappyBunny : Bunny, IBunny
    {
        private const int energy = 100;
        public HappyBunny(string name) : base(name, energy)
        {
        }
    }
}

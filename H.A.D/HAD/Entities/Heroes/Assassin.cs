using HAD.Contracts;

namespace HAD.Entities.Heroes
{
    public class Assassin : BaseHero, IHero
    {
        private const long BaseStrength = 25;
        private const long BaseAgility = 100;
        private const long BaseIntelligence = 15;
        private const long BaseHitPoints = 150;
        private const long BaseDamage = 300;
        public Assassin(string name)
            : base (name, BaseStrength, BaseAgility, BaseIntelligence, BaseHitPoints, BaseDamage)
        {
        }
    }
}

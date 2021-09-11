using HAD.Contracts;

namespace HAD.Entities.Heroes
{
    public class Wizard : BaseHero, IHero
    {
        private const long BaseStrength = 25;
        private const long BaseAgility = 25;
        private const long BaseIntelligence = 100;
        private const long BaseHitPoints = 100;
        private const long BaseDamage = 250;
        public Wizard(string name)
            : base (name, BaseStrength, BaseAgility, BaseIntelligence, BaseHitPoints, BaseDamage)
        {
        }
    }
}

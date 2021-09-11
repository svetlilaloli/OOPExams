using HAD.Contracts;

namespace HAD.Entities.Heroes
{
    public class Barbarian : BaseHero, IHero
    {
        private const long BaseStrength = 90;
        private const long BaseAgility = 25;
        private const long BaseIntelligence = 10;
        private const long BaseHitPoints = 350;
        private const long BaseDamage = 150;

        public Barbarian(string name)
            : base(name, BaseStrength, BaseAgility, BaseIntelligence, BaseHitPoints, BaseDamage)
        { 
        }
    }
}
namespace CarRacing.Models.Racers
{
    using CarRacing.Models.Cars.Contracts;
    using CarRacing.Models.Racers.Contracts;
    public class StreetRacer : Racer, IRacer
    {
        private const string Behavior = "aggressive";
        private const int Experience = 10;
        public StreetRacer(string username, ICar car)
            : base(username, Behavior, Experience, car)
        {
        }
    }
}

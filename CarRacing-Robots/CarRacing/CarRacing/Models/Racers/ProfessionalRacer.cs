namespace CarRacing.Models.Racers
{
    using CarRacing.Models.Cars.Contracts;
    using CarRacing.Models.Racers.Contracts;
    public class ProfessionalRacer : Racer, IRacer
    {
        private const string Behavior = "strict";
        private const int Experience = 30;
        public ProfessionalRacer(string username, ICar car)
            : base(username, Behavior, Experience, car)
        {
        }
    }
}

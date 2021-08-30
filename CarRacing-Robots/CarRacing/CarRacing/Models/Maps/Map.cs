namespace CarRacing.Models.Maps
{
    using CarRacing.Models.Maps.Contracts;
    using CarRacing.Models.Racers.Contracts;
    using CarRacing.Utilities.Messages;
    public class Map : IMap
    {
        private const double StrictBehaviorMultiplier = 1.2;
        private const double AggressiveBehaviorMultiplier = 1.1;
        private const string ProfessionalRacer = "ProfessionalRacer";
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return OutputMessages.RaceCannotBeCompleted;
            }
            if (!racerOne.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }
            if (!racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }
            racerOne.Race();
            racerTwo.Race();

            double racerOneChanceOfWinning = ChanceOfWinning(racerOne);
            double racerTwoChanceOfWinning = ChanceOfWinning(racerTwo);

            return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username,
                racerOneChanceOfWinning > racerTwoChanceOfWinning ? racerOne.Username : racerTwo.Username);
        }
        private static double ChanceOfWinning(IRacer racer)
        {
            string racerType = racer.GetType().Name;
            double chanceOfWinning = racer.Car.HorsePower * racer.DrivingExperience;

            if (racerType == ProfessionalRacer)
            {
                chanceOfWinning *= StrictBehaviorMultiplier;
            }
            else
            {
                chanceOfWinning *= AggressiveBehaviorMultiplier;
            }
            return chanceOfWinning;
        }
    }
}

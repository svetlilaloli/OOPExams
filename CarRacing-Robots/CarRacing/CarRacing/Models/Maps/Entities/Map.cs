namespace CarRacing.Models.Maps.Entities
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
            racerOne.Car.Drive();
            racerOne.Race();
            racerTwo.Car.Drive();
            racerTwo.Race();

            double racerOneChanceOfWinning = ChanceOfWinning(racerOne);
            double racerTwoChanceOfWinning = ChanceOfWinning(racerTwo);
            //TO CHECK OUTPUT AND CALCULATION
            return string.Format(OutputMessages.RacerWinsRace, racerOne, racerTwo,
                racerOneChanceOfWinning > racerTwoChanceOfWinning ? racerOne : racerTwo);
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

namespace CarRacing.Repositories
{
    using CarRacing.Models.Racers.Contracts;
    using CarRacing.Repositories.Contracts;
    using CarRacing.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    public class RacerRepository : IRepository<IRacer>
    {
        private readonly List<IRacer> racers;
        public RacerRepository()
        {
            racers = new List<IRacer>();
        }
        public IReadOnlyCollection<IRacer> Models => racers.AsReadOnly();

        public void Add(IRacer racer)
        {
            if (racer == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddRacerRepository);
            }
            racers.Add(racer);
        }

        public IRacer FindBy(string username)
        {
            return racers.Find(x => x.Username == username);
        }

        public bool Remove(IRacer racer)
        {
            return racers.Remove(racer);
        }
    }
}

using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Eggs;
using Easter.Models.Workshops;
using Easter.Repositories;
using Easter.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace Easter.Core
{
    public class Controller : IController
    {
        private readonly BunnyRepository bunnies;
        private readonly EggRepository eggs;
        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
        }
        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny;
            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            bunnies.Add(bunny);
            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            var bunnyFound = bunnies.FindByName(bunnyName);
            if (bunnyFound == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }
            var dye = new Dye(power);
            bunnyFound.AddDye(dye);

            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            var egg = new Egg(eggName, energyRequired);
            eggs.Add(egg);

            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            var eggFound = eggs.FindByName(eggName);
            var readyBunnies = bunnies.Models.Where(x => x.Energy >= 50).OrderByDescending(x => x.Energy);

            if (readyBunnies.Count() == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            var workshop = new Workshop();
            var eggDone = false;
            foreach (var bunny in readyBunnies)
            {
                workshop.Color(eggFound, bunny);

                if (bunny.Energy == 0)
                {
                    bunnies.Remove(bunny);
                }

                if (eggFound.IsDone())
                {
                    eggDone = true;
                    break;
                }
            }

            if (eggDone)
            {
                return string.Format(OutputMessages.EggIsDone, eggName);
            }
            return string.Format(OutputMessages.EggIsNotDone, eggName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            var countColoredEggs = eggs.Models.Where(x => x.IsDone() == true).Count();
            sb.AppendLine($"{countColoredEggs} eggs are done!");
            sb.Append("Bunnies info:");
            foreach(var bunny in bunnies.Models)
            {
                sb.Append($"\nName: {bunny.Name}");
                sb.Append($"\nEnergy: {bunny.Energy}");
                sb.Append($"\nDyes: {bunny.Dyes.Count} not finished");
            }
            return sb.ToString();
        }
    }
}

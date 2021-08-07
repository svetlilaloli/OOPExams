using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System.Linq;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            while (bunny.Dyes.Count > 0)
            {
                var dye = bunny.Dyes.FirstOrDefault();
                
                while (!egg.IsDone() && dye.Power > 0 && bunny.Energy > 0)
                {
                    bunny.Work();
                    dye.Use();
                    egg.GetColored();
                }
                if (dye.Power == 0)
                {
                    bunny.Dyes.Remove(dye);
                }
                if (egg.IsDone() || bunny.Energy == 0)
                {
                    break;
                }
            }
        }
    }
}

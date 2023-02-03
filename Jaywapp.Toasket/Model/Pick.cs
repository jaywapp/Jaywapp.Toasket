using Jaywapp.Toasket.Interface;

namespace Jaywapp.Toasket.Model
{
    public class Pick
    {
        public IMatch Match { get; }
        public eMatchResult Expect { get; }
        public double Ratio { get; }

        public Pick(IMatch match, eMatchResult expect, double ratio)
        {
            Match = match;
            Expect = expect;
            Ratio = ratio;
        }
    }
}

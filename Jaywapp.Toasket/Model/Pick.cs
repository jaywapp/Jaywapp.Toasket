namespace Jaywapp.Toasket.Model
{
    public class Pick
    {
        public Match Match { get; }
        public eMatchResult Expect { get; }
        public double Ratio { get; }

        public Pick(Match match, eMatchResult expect, double ratio)
        {
            Match = match;
            Expect = expect;
            Ratio = ratio;
        }
    }
}

using System;

namespace Jaywapp.Toasket.Model
{
    public class Match 
    {
        public int No { get; set; }
        public DateTime Time { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public double Win { get; set; }
        public double Draw { get; set; }
        public double Lose { get; set; }
        public ePick Pick { get; set; }

        public Match(int no, DateTime time, string home, string away, double win, double draw, double lose)
        {
            No = no;
            Time = time;
            Home = home;
            Away = away;

            Win = win;
            Draw = draw;
            Lose = lose;
        }

        public double GetRatio()
        {
            switch (Pick)
            {
                case ePick.Win: return Win;
                case ePick.Draw: return Draw;
                case ePick.Lose: return Lose;
                case ePick.None:
                default:
                    return 1;
            }
        }

        public Match Copy()
        {
            var match = new Match(No, Time, Home, Away, Win, Draw, Lose);
            match.Pick = Pick;

            return match;
        }
    }
}

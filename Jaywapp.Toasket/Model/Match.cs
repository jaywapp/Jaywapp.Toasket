using Jaywapp.Toasket.Interface;
using System;

namespace Jaywapp.Toasket.Model
{
    public class Match : IMatch
    {
        public int No { get; set; }
        public DateTime Time { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public double Win { get; set; }
        public double Draw { get; set; }
        public double Lose { get; set; }

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
    }
}

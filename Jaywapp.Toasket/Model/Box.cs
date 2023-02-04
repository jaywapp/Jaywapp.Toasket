using Jaywapp.Toasket.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Toasket.Model
{
    public class Box
    {
        public DateTime Created { get; }
        public List<Match> Picks { get; } = new List<Match>();
        public int Money { get; set; }

        public Box(IEnumerable<Match> picks, int money)
        {
            Created = DateTime.Now;
            Picks = picks.ToList();
            Money = money;
        }

        public double GetRatio() => Calculater.Multiply(Picks, p => p.GetRatio());

        public int GetReturnMoney() => (int)(GetRatio() * Money);
    }
}

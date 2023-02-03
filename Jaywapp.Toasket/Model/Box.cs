﻿using Jaywapp.Toasket.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaywapp.Toasket.Model
{
    public class Box
    {
        public List<Pick> Picks { get; } = new List<Pick>();
        public int Money { get; }
        public double Ratio { get; }
        public int ReturnMoney { get; }

        public Box(IEnumerable<Pick> picks, int money)
        {
            Picks = picks.ToList();

            Ratio =  Calculater.Multiply(Picks, p => p.Ratio);
            Money = money;
            ReturnMoney = (int)(Ratio * Money);
        }
    }
}

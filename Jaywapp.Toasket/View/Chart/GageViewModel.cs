using Jaywapp.Toasket.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Toasket.View.Chart
{
    public class GageViewModel : ReactiveObject
    {
        private double _hazard = 0;

        public double Hazard
        {
            get => _hazard;
            private set => this.RaiseAndSetIfChanged(ref _hazard, value);
        }

        public GageViewModel()
        {
        }

        public void Update(IEnumerable<Box> boxes)
        {
            // 총지출
            var expenditure = boxes.Sum(b => b.Money);
            // 순이익
            var prohit = boxes.Sum(b => b.GetProhit());

            Hazard = CalculateHazard(expenditure, prohit);
        }


        private static double CalculateHazard(int expenditure, int prohit)
        {
            var value = prohit - expenditure;

            if (value < 0)
                return 100;
            else if (value == 0)
                return 40;
            else
                return 0;
        }

    }
}

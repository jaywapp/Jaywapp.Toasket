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
        }
    }
}

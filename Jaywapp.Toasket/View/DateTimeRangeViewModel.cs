using ReactiveUI;
using System;

namespace Jaywapp.Toasket.View
{
    public class DateTimeRangeViewModel : ReactiveObject
    {
        private DateTime _start = DateTime.Today;
        private DateTime _end = DateTime.Today;

        public DateTime Start
        {
            get => _start;
            set => this.RaiseAndSetIfChanged(ref _start, value);
        }

        public DateTime End
        {
            get => _end;
            set => this.RaiseAndSetIfChanged(ref _end, value);
        }
    }
}

using ReactiveUI;
using System;

namespace Jaywapp.Toasket.Items
{
    public class DateTimeRange : ReactiveObject
    {
        private DateTime _start;
        private DateTime _end;

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

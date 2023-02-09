using ReactiveUI;
using System;

namespace Jaywapp.Toasket.Items
{
    public class DateTimeRange : ReactiveObject
    {
        #region Internal Field
        private DateTime _start;
        private DateTime _end;
        #endregion

        #region Properties
        /// <summary>
        /// Start Time
        /// </summary>
        public DateTime Start
        {
            get => _start;
            set => this.RaiseAndSetIfChanged(ref _start, value);
        }

        /// <summary>
        /// End Time
        /// </summary>
        public DateTime End
        {
            get => _end;
            set => this.RaiseAndSetIfChanged(ref _end, value);
        }
        #endregion
    }
}

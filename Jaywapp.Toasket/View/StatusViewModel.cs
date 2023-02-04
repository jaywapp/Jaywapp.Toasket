using ReactiveUI;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View
{
    public class StatusViewModel : ReactiveObject
    {
        #region Internal Field
        private int _count;
        private double _ratio;
        private int _money;
        private int _returnMoney;
        #endregion

        #region Properties
        public int Count
        {
            get => _count;
            set => this.RaiseAndSetIfChanged(ref _count, value);
        }

        public double Ratio
        {
            get => _ratio;
            set => this.RaiseAndSetIfChanged(ref _ratio, value);
        }

        public int Money
        {
            get => _money;
            set => this.RaiseAndSetIfChanged(ref _money, value);
        }

        public int ReturnMoney
        {
            get => _returnMoney;
            set => this.RaiseAndSetIfChanged(ref _returnMoney, value);
        }
        #endregion

        #region Constructor
        public StatusViewModel()
        {
        }
        #endregion
    }
}

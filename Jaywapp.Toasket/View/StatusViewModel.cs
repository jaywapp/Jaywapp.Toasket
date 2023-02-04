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
        private ObservableAsPropertyHelper<int> _returnMoney;
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

        public int ReturnMoney => _returnMoney.Value;
        #endregion

        #region Constructor
        public StatusViewModel()
        {
            this.WhenAnyValue(x => x.Money, x => x.Ratio)
                .Select(p => Calculate(p.Item1, p.Item2))
                .ToProperty(this, x => x.ReturnMoney, out _returnMoney);
        }
        #endregion

        #region Functions
        private static int Calculate(int money, double ratio) => (int)(money * ratio);
        #endregion
    }
}

using DynamicData;
using DynamicData.Binding;
using Jaywapp.Toasket.Helper;
using Jaywapp.Toasket.Model;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.Items
{
    public class BoxItem : ReactiveObject
    {
        #region Internal Field
        private int _money = 0;
        private ObservableAsPropertyHelper<double> _ratio;
        private ObservableAsPropertyHelper<int> _returnMoney;
        #endregion

        #region Properties
        public Box Box { get; }

        public int Money
        {
            get => _money;
            set => this.RaiseAndSetIfChanged(ref _money, value);
        }

        public double Ratio => _ratio.Value;
        
        public int ReturnMoney => _returnMoney.Value;

        public ObservableCollection<MatchItem> Children { get; }
        #endregion

        #region Constructor
        public BoxItem(Box box)
        {
            Box = box;
            Money = box.Expenditure;
            Children = box.Picks
                .Select(p => new MatchItem(p))
                .ToObservableCollection();

            this.WhenAnyValue(x => x.Money)
                .BindTo(this, x => x.Box.Expenditure);
            this.WhenAnyValue(x => x.Money)
                 .Select(p => Box.GetRatio())
                 .ToProperty(this, x => x.Ratio, out _ratio);

            Children.ToObservableChangeSet()
                .AutoRefresh(c => c.Pick)
                .Throttle(TimeSpan.FromSeconds(0.01))
                .Select(p => Box.GetRatio())
                .ToProperty(this, x => x.Ratio, out _ratio);

            this.WhenAnyValue(x => x.Money, x => x.Ratio)
                .Select(p => Box.Income)
                .ToProperty(this, x => x.ReturnMoney, out _returnMoney);
        }
        #endregion
    }
}

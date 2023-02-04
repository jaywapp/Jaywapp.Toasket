using Jaywapp.Toasket.Helper;
using Jaywapp.Toasket.Items;
using Jaywapp.Toasket.View.List;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View
{
    public class BoxItemConfigViewModel : ReactiveObject
    {
        #region Internal Field
        private BoxItem _item;

        private int _count;
        private double _ratio;
        private int _money;
        private int _returnMoney;
        #endregion

        #region Properties
        public BoxItem Item
        {
            get => _item;
            set => this.RaiseAndSetIfChanged(ref _item, value);
        }

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

        #region ViewModels
        public StatusViewModel StatusViewModel { get; }
        public MatchItemListViewModel MatchItemListViewModel { get; }
        #endregion

        #region Constructor
        public BoxItemConfigViewModel(BoxItem item)
        {
            Item = item;
            StatusViewModel = new StatusViewModel();
            MatchItemListViewModel = new MatchItemListViewModel();

            var itemChanges = this.WhenAnyValue(x => x.Item).Where(i => i != null);

            itemChanges
                .Select(i => i.Children)
                .BindTo(this, x => x.MatchItemListViewModel.Items);

            item.WhenAnyValue(i => i.Money)
                .BindTo(this, x => x.StatusViewModel.Money);
            item.WhenAnyValue(i => i.Children)
                .Select(c => c.Count)
                .BindTo(this, x => x.StatusViewModel.Count);
            item.WhenAnyValue(i => i.Ratio)
                .BindTo(this, x => x.StatusViewModel.Ratio);

            StatusViewModel.WhenAnyValue(x => x.Money)
                .BindTo(this, x => x.Item.Money);
        }
        #endregion
    }
}

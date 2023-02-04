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
        #endregion

        #region Properties
        public BoxItem Item
        {
            get => _item;
            set => this.RaiseAndSetIfChanged(ref _item, value);
        }
        #endregion

        #region ViewModels
        public StatusViewModel StatusViewModel { get; }
        public MatchItemListViewModel MatchItemListViewModel { get; }
        #endregion

        #region Constructor
        public BoxItemConfigViewModel()
        {
            StatusViewModel = new StatusViewModel();
            MatchItemListViewModel = new MatchItemListViewModel();

            var itemChanges = this.WhenAnyValue(x => x.Item).Where(i => i != null);

            itemChanges
                .Select(item => item.Children)
                .BindTo(this, x => x.MatchItemListViewModel.Items);

            itemChanges.Subscribe(item =>
            {
                item.WhenAnyValue(i => i.Money)
                    .BindTo(this, x => x.StatusViewModel.Money);
                item.WhenAnyValue(i => i.Children)
                    .Select(c => c.Count)
                    .BindTo(this, x => x.StatusViewModel.Count);
                item.WhenAnyValue(i => i.Ratio)
                    .BindTo(this, x => x.StatusViewModel.Ratio);
            });

            StatusViewModel.WhenAnyValue(x => x.Money)
                .BindTo(this, x => x.Item.Money);
        }
        #endregion
    }
}

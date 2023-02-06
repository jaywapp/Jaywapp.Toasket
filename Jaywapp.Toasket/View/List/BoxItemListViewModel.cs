using Jaywapp.Toasket.Items;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace Jaywapp.Toasket.View.List
{
    public class BoxItemListViewModel : ReactiveObject
    {
        #region Internal Field
        private ObservableCollection<BoxItem> _items = new ObservableCollection<BoxItem>();
        private BoxItem _selectedItem;
        #endregion

        #region Properties
        public ObservableCollection<BoxItem> Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        public BoxItem SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }
        #endregion
    }
}

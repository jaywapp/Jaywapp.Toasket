using Jaywapp.Toasket.Items;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaywapp.Toasket.View
{
    public class MatchItemListViewModel : ReactiveObject
    {
        private ObservableCollection<MatchItem> _items = new ObservableCollection<MatchItem>();

        public ObservableCollection<MatchItem> Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }
    }
}

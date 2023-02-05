﻿using DynamicData;
using DynamicData.Binding;
using Jaywapp.Toasket.Items;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View.List
{
    public class MatchItemListViewModel : ReactiveObject
    {
        private ObservableCollection<MatchItem> _items = new ObservableCollection<MatchItem>();

        public ObservableCollection<MatchItem> Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        public MatchItemListViewModel()
        {
        }
    }
}

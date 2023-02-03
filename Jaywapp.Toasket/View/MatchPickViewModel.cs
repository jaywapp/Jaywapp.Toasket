using DynamicData;
using DynamicData.Binding;
using Jaywapp.Toasket.Helper;
using Jaywapp.Toasket.Items;
using Jaywapp.Toasket.Model;
using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Microsoft.Practices.Unity;
using Prism.Commands;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View
{
    public class MatchPickViewModel : ContainableReactiveObject
    {
        private ObservableAsPropertyHelper<List<MatchItem>> _selectedItems;
        private ObservableAsPropertyHelper<int> _selectionCount;
        private ObservableAsPropertyHelper<double> _selectionRatio;
        private int _money = 0;

        private ObservableAsPropertyHelper<bool> _isConfirmable;
        private ObservableAsPropertyHelper<int> _returnMoney ;

        public List<MatchItem> SelectedItems => _selectedItems.Value;
        public int SelectionCount => _selectionCount.Value;
        public double SelectionRatio => _selectionRatio.Value;
        public bool IsConfirmable => _isConfirmable.Value;
        public int ReturnMoney => _returnMoney.Value;

        public int Money
        {
            get => _money;
            set => this.RaiseAndSetIfChanged(ref _money, value);
        }

        public MatchItemListViewModel MatchItemListViewModel { get; }

        public DelegateCommand ConfirmCommand { get; }


        public MatchPickViewModel(IUnityContainer container, MatchRepository dataRepository)
            : base(container, dataRepository)
        {
            ConfirmCommand = new DelegateCommand(Confirm);

            MatchItemListViewModel = new MatchItemListViewModel();

            var matches = _dataRepository.Matches
              .Select(d => new MatchItem(d))
              .ToList();

            MatchItemListViewModel.Items = new ObservableCollection<MatchItem>(matches);

            MatchItemListViewModel.Items
                .ToObservableChangeSet()
                .AutoRefresh(i => i.SelectedResult)
                .Throttle(TimeSpan.FromSeconds(0.01))
                .ToCollection()
                .Select(col => col.Where(c => c.SelectedResult != eMatchResult.None).ToList())
                .ToProperty(this, x => x.SelectedItems, out _selectedItems);

            this.WhenAnyValue(x => x.SelectedItems)
                .Select(items => items.Count)
                .ToProperty(this, x => x.SelectionCount, out _selectionCount);

            this.WhenAnyValue(x => x.SelectedItems)
                .Select(items => Calculater.Multiply(items, i => i.GetRatio()))
                .ToProperty(this, x => x.SelectionRatio, out _selectionRatio);

            this.WhenAnyValue(x => x.SelectionRatio, x => x.Money)
                .Select(p => (int)(p.Item1 * p.Item2))
                .ToProperty(this, x => x.ReturnMoney, out _returnMoney);

            this.WhenAnyValue(x => x.SelectedItems, x => x.Money)
                .Select(p => p.Item1.Any() && p.Item2 != 0)
                .ToProperty(this, x => x.IsConfirmable, out _isConfirmable);
        }

        private void Confirm()
        {
            var box = new Box(SelectedItems.Select(i => i.ToPick()), Money);
            
            // Next Job
        }
    }
}

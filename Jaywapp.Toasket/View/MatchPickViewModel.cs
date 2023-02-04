using DynamicData;
using DynamicData.Binding;
using Jaywapp.Toasket.Helper;
using Jaywapp.Toasket.Items;
using Jaywapp.Toasket.Model;
using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Jaywapp.Toasket.View.List;
using Microsoft.Practices.Unity;
using Prism.Commands;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View
{
    public class MatchPickViewModel : ContainableReactiveObject
    {
        #region Internal Field
        private ObservableAsPropertyHelper<List<MatchItem>> _selectedItems;
        private ObservableAsPropertyHelper<bool> _isConfirmable;
        #endregion

        #region Properties
        public List<MatchItem> SelectedItems => _selectedItems.Value;
        public bool IsConfirmable => _isConfirmable.Value;
        #endregion

        #region ViewModels
        public MatchItemListViewModel MatchItemListViewModel { get; }
        public StatusViewModel StatusViewModel { get; }
        #endregion

        #region Command
        public DelegateCommand ConfirmCommand { get; }
        #endregion

        #region Constructor
        public MatchPickViewModel(IUnityContainer container, MatchRepository dataRepository, PersonalRepository personalRepository)
            : base(container, dataRepository, personalRepository)
        {
            ConfirmCommand = new DelegateCommand(Confirm);

            MatchItemListViewModel = new MatchItemListViewModel();
            StatusViewModel = new StatusViewModel();

            MatchItemListViewModel.Items = _dataRepository.Matches
                .Select(d => new MatchItem(d))
                .ToObservableCollection();

            MatchItemListViewModel.Items
                .ToObservableChangeSet()
                .AutoRefresh(i => i.Pick)
                .Throttle(TimeSpan.FromSeconds(0.01))
                .ToCollection()
                .Select(col => col.Where(c => c.Pick != ePick.None).ToList())
                .ToProperty(this, x => x.SelectedItems, out _selectedItems);

            this.WhenAnyValue(x => x.SelectedItems)
                .Select(items => items.Count)
                .BindTo(this, x => x.StatusViewModel.Count);

            this.WhenAnyValue(x => x.SelectedItems)
                .Select(items => Calculater.Multiply(items, i => i.Match.GetRatio()))
                .BindTo(this, x => x.StatusViewModel.Ratio);

            this.WhenAnyValue(x => x.SelectedItems, x => x.StatusViewModel.Money)
                .Select(p => p.Item1.Any() && p.Item2 != 0)
                .ToProperty(this, x => x.IsConfirmable, out _isConfirmable);
        }
        #endregion

        #region Functions
        private void Confirm()
        {
            var matchs = SelectedItems.Select(i => i.Match.Copy());
            var money = StatusViewModel.Money;

            var box = new Box(matchs, money);

            _personalRepository.Add(box);

            foreach (var selectedItem in SelectedItems)
                selectedItem.Pick = ePick.None;

            StatusViewModel.Money = 0;
        }
        #endregion
    }
}

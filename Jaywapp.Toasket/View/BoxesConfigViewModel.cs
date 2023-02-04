using Jaywapp.Toasket.Helper;
using Jaywapp.Toasket.Items;
using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.List;
using Prism.Commands;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View
{
    public class BoxesConfigViewModel : ReactiveObject
    {
        private readonly PersonalRepository _personalRepository;
        private ObservableAsPropertyHelper<bool> _isActiveSelection;
        private ObservableAsPropertyHelper<BoxItem> _selectedItem;

        public bool IsActiveSelection => _isActiveSelection.Value;

        public BoxItem SelectedItem => _selectedItem.Value;

        public DelegateCommand DeleteCommand { get; }

        public BoxItemListViewModel BoxItemListViewModel { get; }

        public BoxesConfigViewModel(PersonalRepository personalRepository)
        {
            _personalRepository = personalRepository ?? throw new ArgumentNullException(nameof(personalRepository));

            DeleteCommand = new DelegateCommand(Delete);
            
            BoxItemListViewModel = new BoxItemListViewModel();
            BoxItemListViewModel.WhenAnyValue(x => x.SelectedItem)
                .ToProperty(this, x => x.SelectedItem, out _selectedItem);

            this.WhenAnyValue(x => x.SelectedItem)
                .Select(i => i != null)
                .ToProperty(this, x => x.IsActiveSelection, out _isActiveSelection);

            personalRepository.Updated += OnUpdate;
            OnUpdate(personalRepository, EventArgs.Empty);
        }

        private void Delete()
        {
            _personalRepository.Delete(
                BoxItemListViewModel.SelectedItem.Box);
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            if (!(sender is PersonalRepository repository))
                return;

            BoxItemListViewModel.Items = repository.Boxes
                .Select(b => new BoxItem(b))
                .ToObservableCollection();
        }
    }
}

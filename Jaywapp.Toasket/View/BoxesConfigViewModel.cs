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
        #region Internal Field
        private readonly PersonalRepository _personalRepository;

        private ObservableAsPropertyHelper<bool> _isActiveSelection;
        private ObservableAsPropertyHelper<BoxItem> _selectedItem;
        #endregion

        #region Properties
        public bool IsActiveSelection => _isActiveSelection.Value;
        public BoxItem SelectedItem => _selectedItem.Value;
        #endregion

        #region Commands
        public DelegateCommand DeleteCommand { get; }
        #endregion

        #region ViewModels
        public BoxItemListViewModel BoxItemListViewModel { get; }
        #endregion

        #region Constructor
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
        #endregion

        #region Functions
        private void Delete()
        {
            _personalRepository.Delete(BoxItemListViewModel.SelectedItem.Box);
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            if (!(sender is PersonalRepository repository))
                return;

            BoxItemListViewModel.Items = repository.Boxes
                .Select(b => new BoxItem(b))
                .ToObservableCollection();
        }
        #endregion
    }
}

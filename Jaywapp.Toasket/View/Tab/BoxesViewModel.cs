using Jaywapp.Toasket.Items;
using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Microsoft.Practices.Unity;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View.Tab
{
    public class BoxesViewModel : ContainableReactiveObject
    {
        #region Internal Field
        private BoxItemConfigViewModel _boxItemConfigViewModel;
        private Dictionary<BoxItem, BoxItemConfigViewModel> _dic = new Dictionary<BoxItem, BoxItemConfigViewModel>();
        #endregion

        #region ViewModels
        public BoxesConfigViewModel BoxesConfigViewModel { get; }
        public BoxItemConfigViewModel BoxItemConfigViewModel 
        {
            get => _boxItemConfigViewModel;
            set 
            {
                SwitchEvent(_boxItemConfigViewModel, value);
                this.RaiseAndSetIfChanged(ref _boxItemConfigViewModel, value);
            }
        }
        #endregion

        #region Constructor
        public BoxesViewModel(IUnityContainer container, MatchRepository matchRepo, PersonalRepository personalRepo)
            : base(container, matchRepo, personalRepo)
        {
            BoxesConfigViewModel = new BoxesConfigViewModel(personalRepo);
            BoxesConfigViewModel.WhenAnyValue(x => x.SelectedItem)
                .Where(item => item != null)
                .Select(GetViewModel)
                .BindTo(this, x => x.BoxItemConfigViewModel);
        }
        #endregion

        #region Functions
        private BoxItemConfigViewModel GetViewModel(BoxItem item)
        {
            if (!_dic.ContainsKey(item))
                _dic.Add(item, new BoxItemConfigViewModel(item));

            return _dic[item];
        }

        private void OnBoxItemContentChanged(object sender, EventArgs e)
        {
            _personalRepo.Refresh();
        }

        private void SwitchEvent(BoxItemConfigViewModel oldTarget, BoxItemConfigViewModel newTarget)
        {
            oldTarget.ContentChagned -= OnBoxItemContentChanged;
            newTarget.ContentChagned += OnBoxItemContentChanged;
        }
        #endregion
    }
}

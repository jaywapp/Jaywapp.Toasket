using Jaywapp.Toasket.Items;
using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Microsoft.Practices.Unity;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View.Tab
{
    public class BoxesViewModel : ContainableReactiveObject
    {
        #region Internal Field
        private ObservableAsPropertyHelper<BoxItemConfigViewModel> _boxItemConfigViewModel;
        private Dictionary<BoxItem, BoxItemConfigViewModel> _dic = new Dictionary<BoxItem, BoxItemConfigViewModel>();
        #endregion

        #region ViewModels
        public BoxesConfigViewModel BoxesConfigViewModel { get; }
        public BoxItemConfigViewModel BoxItemConfigViewModel => _boxItemConfigViewModel.Value;
        #endregion

        #region Constructor
        public BoxesViewModel(IUnityContainer container, MatchRepository matchRepo, PersonalRepository personalRepo)
            : base(container, matchRepo, personalRepo)
        {
            BoxesConfigViewModel = new BoxesConfigViewModel(personalRepo);
            BoxesConfigViewModel.WhenAnyValue(x => x.SelectedItem)
                .Where(item => item != null)
                .Select(GetViewModel)
                .ToProperty(this, x => x.BoxItemConfigViewModel, out _boxItemConfigViewModel);
        }

        private BoxItemConfigViewModel GetViewModel(BoxItem item)
        {
            if (!_dic.ContainsKey(item))
                _dic.Add(item, new BoxItemConfigViewModel(item));

            return _dic[item];
        }
        #endregion
    }
}

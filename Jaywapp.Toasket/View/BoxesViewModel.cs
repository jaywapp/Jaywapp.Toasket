using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Microsoft.Practices.Unity;
using Jaywapp.Toasket.Items;
using Prism.Commands;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
using Jaywapp.Toasket.Helper;
using Jaywapp.Toasket.View.List;

namespace Jaywapp.Toasket.View
{
    public class BoxesViewModel : ContainableReactiveObject
    {
        public BoxesConfigViewModel BoxesConfigViewModel { get; }
        public BoxItemConfigViewModel BoxItemConfigViewModel { get; }


        public BoxesViewModel(IUnityContainer container, MatchRepository dataRepository, PersonalRepository personalRepository)
            : base(container, dataRepository, personalRepository)
        {
            BoxesConfigViewModel = new BoxesConfigViewModel(personalRepository);
            BoxItemConfigViewModel = new BoxItemConfigViewModel();

            BoxesConfigViewModel.WhenAnyValue(x => x.SelectedItem)
                .BindTo(this, x => x.BoxItemConfigViewModel.Item);
        }
    }
}

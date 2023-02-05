using Jaywapp.Toasket.View.Tab;
using Microsoft.Practices.Unity;
using Prism.Commands;
using ReactiveUI;
using System;
using System.Windows.Controls;

namespace Jaywapp.Toasket
{
    public class ShellViewModel : ReactiveObject
    {
        #region Internal Field
        private readonly IUnityContainer _container;
        private Control _activeView;
        #endregion

        #region Properties
        public Control ActiveView
        {
            get => _activeView;
            set => this.RaiseAndSetIfChanged(ref _activeView, value);
        }
        #endregion

        #region Commands
        public DelegateCommand ActiveHomeViewCommand { get; }
        public DelegateCommand ActiveMatchViewCommand { get; }
        public DelegateCommand ActiveBasketViewCommand { get; }
        public DelegateCommand ActiveAnalysisViewCommand { get; }
        #endregion

        #region Constructor
        public ShellViewModel(IUnityContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));

            ActiveHomeViewCommand = new DelegateCommand(() => ActivateView<HomeView>());
            ActiveMatchViewCommand = new DelegateCommand(() => ActivateView<MatchPickView>());
            ActiveBasketViewCommand = new DelegateCommand(() => ActivateView<BoxesView>());
            ActiveAnalysisViewCommand = new DelegateCommand(() => ActivateView<AnalysisView>());

            ActivateView<HomeView>();
        }
        #endregion

        #region Functions
        private void ActivateView<TView>()
            where TView : Control
        {
            ActiveView = _container.Resolve<TView>();
        }
        #endregion
    }
}

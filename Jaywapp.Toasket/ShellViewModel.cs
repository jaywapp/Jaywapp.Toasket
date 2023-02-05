using Jaywapp.Toasket.View.Tab;
using Microsoft.Practices.Unity;
using Prism.Commands;
using ReactiveUI;
using System;
using System.Windows.Controls;

namespace Jaywapp.Toasket
{
    public class ShellViewModel : ReactiveObject, IDisposable
    {
        #region Internal Field
        private readonly IUnityContainer _container;
        private Control _activeView;

        private bool _isBusy;
        private string _busyContent;
        #endregion

        #region Properties
        public Control ActiveView
        {
            get => _activeView;
            set => this.RaiseAndSetIfChanged(ref _activeView, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => this.RaiseAndSetIfChanged(ref _isBusy, value);
        }

        public string BusyContent
        {
            get => _busyContent;
            set => this.RaiseAndSetIfChanged(ref _busyContent, value);
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

        public void Start(string content)
        {
            BusyContent = content;
            IsBusy = true;
        }

        public void End()
        {
            BusyContent = string.Empty;
            IsBusy = false;
        }

        public void Dispose()
        {
        }
        #endregion
    }
}

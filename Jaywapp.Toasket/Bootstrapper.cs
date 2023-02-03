using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Unity;
using ReactiveUI;
using System.Windows;
using System.Windows.Controls;

namespace Jaywapp.Toasket
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Register<MatchRepository>();

            Register<HomeView, HomeViewModel>();
            Register<MatchView, MatchViewModel>();
            Register<BasketView, BasketViewModel>();
            Register<AnalysisView, AnalysisViewModel>();
        }

        private void Register<T>()
        {
            Container.RegisterType<T>(new ContainerControlledLifetimeManager());
        }

        private void Register<TView, TViewModel>()
            where TView : Control
            where TViewModel : ReactiveObject
        {
            Container.RegisterType<TView>(new ContainerControlledLifetimeManager());
            Container.RegisterType<TViewModel>(new ContainerControlledLifetimeManager());

            ViewModelLocationProvider.Register<TView, TViewModel>();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            var mainWindow = Application.Current.MainWindow;
            mainWindow.Show();
            mainWindow.Activate();
        }
    }
}

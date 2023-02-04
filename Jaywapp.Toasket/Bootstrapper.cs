using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View;
using Jaywapp.Toasket.View.Tab;
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
            Register<PersonalRepository>();

            Register<HomeView, HomeViewModel>();
            Register<MatchPickView, MatchPickViewModel>();
            Register<BoxesView, BoxesViewModel>();
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

using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Microsoft.Practices.Unity;

namespace Jaywapp.Toasket.View
{
    public class HomeViewModel : ContainableReactiveObject
    {
        public HomeViewModel(IUnityContainer container, MatchRepository dataRepository) : base(container, dataRepository)
        {
        }
    }
}

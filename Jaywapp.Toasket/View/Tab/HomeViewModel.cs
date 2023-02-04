using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Microsoft.Practices.Unity;

namespace Jaywapp.Toasket.View.Tab
{
    public class HomeViewModel : ContainableReactiveObject
    {
        public HomeViewModel(IUnityContainer container, MatchRepository dataRepository, PersonalRepository personalRepository)
            : base(container, dataRepository, personalRepository)
        {
        }
    }
}

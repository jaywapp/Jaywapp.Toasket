using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Microsoft.Practices.Unity;

namespace Jaywapp.Toasket.View
{
    public class BasketViewModel : ContainableReactiveObject
    {
        public BasketViewModel(IUnityContainer container, MatchRepository dataRepository) : base(container, dataRepository)
        {
        }
    }
}

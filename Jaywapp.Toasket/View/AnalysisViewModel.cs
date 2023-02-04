using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Microsoft.Practices.Unity;
using ReactiveUI;

namespace Jaywapp.Toasket.View
{
    public class AnalysisViewModel : ContainableReactiveObject
    {
        public AnalysisViewModel(IUnityContainer container, MatchRepository dataRepository, PersonalRepository personalRepository) 
            : base(container, dataRepository, personalRepository)
        {
        }
    }
}

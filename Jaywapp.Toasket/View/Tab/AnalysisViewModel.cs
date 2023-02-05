using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Analysis;
using Jaywapp.Toasket.View.Base;
using Microsoft.Practices.Unity;

namespace Jaywapp.Toasket.View.Tab
{
    public class AnalysisViewModel : ContainableReactiveObject
    {
        public EarningViewModel EarningViewModel { get; }
        public SpendingViewModel SpendingViewModel { get; }

        public AnalysisViewModel(IUnityContainer container, MatchRepository matchRepo, PersonalRepository personalRepo) 
            : base(container, matchRepo, personalRepo)
        {
            EarningViewModel = new EarningViewModel();
            SpendingViewModel = new SpendingViewModel();
        }
    }
}

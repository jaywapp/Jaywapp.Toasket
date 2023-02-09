using Jaywapp.Toasket.Items;
using Jaywapp.Toasket.Model;
using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.Service;
using Jaywapp.Toasket.View.Base;
using Jaywapp.Toasket.View.Chart;
using Microsoft.Practices.Unity;
using ReactiveUI;
using System;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View.Tab
{
    public class AnalysisViewModel : ContainableReactiveObject
    {
        #region Internal Field
        private Analyst _analyst = null;
        private AnalysisResultGroup _analysisResult = null;
        #endregion

        #region Properties
        public Analyst Analyst
        {
            get => _analyst;
            set => this.RaiseAndSetIfChanged(ref _analyst, value);
        }

        public AnalysisResultGroup AnalysisResult
        {
            get => _analysisResult;
            set => this.RaiseAndSetIfChanged(ref _analysisResult, value);
        }
        #endregion

        #region ViewModels
        public GageViewModel GageViewModel { get; } = new GageViewModel();
        public DateTimeRangeViewModel DateTimeRangeViewModel { get; } = new DateTimeRangeViewModel();
        public SummaryViewModel SummaryViewModel { get; } = new SummaryViewModel();
        public GraphViewModel GraphViewModel { get; } = new GraphViewModel();
        #endregion

        #region Internal Field
        public AnalysisViewModel(IUnityContainer container, MatchRepository matchRepo, PersonalRepository personalRepo) 
            : base(container, matchRepo, personalRepo)
        {
            _personalRepo.Updated += OnUpdate;
            _personalRepo.Refresh();

            DateTimeRangeViewModel
                .WhenAnyValue(x => x.Start, x => x.End)
                .Where(p => Analyst != null)
                .Select(p => Analyst.Analysis(p.Item1, p.Item2))
                .Subscribe(r => AnalysisResult = r);

            this.WhenAnyValue(x=> x.Analyst)
                .Where(a => a != null)
                .Select(a => a.Analysis(DateTimeRangeViewModel.Start, DateTimeRangeViewModel.End))
                .Subscribe(r => AnalysisResult = r);

            this.WhenAnyValue(x => x.AnalysisResult)
                .BindTo(this, x => x.SummaryViewModel.Result);

            this.WhenAnyValue(x => x.AnalysisResult)
                .BindTo(this, x => x.GraphViewModel.Result);
        }
        #endregion

        #region Functions
        private void OnUpdate(object sender, EventArgs e)
        {
            if (!(sender is PersonalRepository personal))
                return;

            Analyst = new Analyst(personal.Boxes);
        }
        #endregion
    }
}

using Jaywapp.Toasket.Model;
using ReactiveUI;

namespace Jaywapp.Toasket.View.Chart
{
    public class SummaryViewModel : ReactiveObject
    {
        private AnalysisResultGroup _result;

        public AnalysisResultGroup Result
        {
            get => _result;
            set => this.RaiseAndSetIfChanged(ref _result, value);
        }
    }
}

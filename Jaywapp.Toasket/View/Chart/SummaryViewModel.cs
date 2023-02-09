using Jaywapp.Toasket.Model;
using ReactiveUI;

namespace Jaywapp.Toasket.View.Chart
{
    public class SummaryViewModel : ReactiveObject
    {
        private AnalysisResult _result;

        public AnalysisResult Result
        {
            get => _result;
            set => this.RaiseAndSetIfChanged(ref _result, value);
        }
    }
}

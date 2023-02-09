using Jaywapp.Toasket.Model;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View.Chart
{
    public class GraphViewModel : ReactiveObject
    {
        private AnalysisResultGroup _result;

        private ObservableAsPropertyHelper<SeriesCollection> _seriesCollection;
        private ObservableAsPropertyHelper<ObservableCollection<string>> _months;


        public AnalysisResultGroup Result
        {
            get => _result;
            set => this.RaiseAndSetIfChanged(ref _result, value);
        }

        public SeriesCollection SeriesCollection => _seriesCollection.Value;

        public ObservableCollection<string> Months => _months.Value;


        public GraphViewModel()
        {
            this.WhenAnyValue(x => x.Result)
                .Where(r => r != null)
                .Select(CreateSeriesCollection)
                .ToProperty(this, x => x.SeriesCollection, out _seriesCollection);

            this.WhenAnyValue(x=> x.Result)
                .Where(r => r != null)
                .Select(CreateMonths)
                .ToProperty(this, x => x.Months, out _months);
        }

        private ObservableCollection<string> CreateMonths(AnalysisResultGroup result)
        {
            var collection = new ObservableCollection<string>();

            collection.AddRange(
                result.Children.Select(m => m.Key.ToString("yyyy - MM")));

            return collection;
        }

        private SeriesCollection CreateSeriesCollection(AnalysisResultGroup result)
        {
            var collection = new SeriesCollection();

            var incomes = result.Children.Values.Select(m => m.Income).AsChartValues();
            var expenditures = result.Children.Values.Select(m => m.Expenditure).AsChartValues();
            var profits = result.Children.Values.Select(m => m.Profit).AsChartValues();

            collection.Add(new ColumnSeries
            {
                Title = "수익",
                Values = incomes,
            });

            collection.Add(new ColumnSeries
            {
                Title = "지출",
                Values = expenditures,
            });

            collection.Add(new ColumnSeries
            {
                Title = "순수익",
                Values = profits,
            });

            return collection;
        }
    }
}

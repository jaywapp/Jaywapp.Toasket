using Jaywapp.Toasket.Model;
using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Jaywapp.Toasket.View.Chart;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Practices.Unity;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.View.Tab
{
    public class AnalysisViewModel : ContainableReactiveObject
    {
        #region Internal Field
        private SeriesCollection _seriesCollection = new SeriesCollection();
        private string[] _months = new string[] { };
        #endregion

        #region Properties
        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set => this.RaiseAndSetIfChanged(ref _seriesCollection, value);
        }

        public string[] Months
        {
            get => _months;
            set => this.RaiseAndSetIfChanged(ref _months, value);
        }
        #endregion

        #region ViewModels
        public GageViewModel GageViewModel { get; } = new GageViewModel();
        #endregion

        #region Internal Field
        public AnalysisViewModel(IUnityContainer container, MatchRepository matchRepo, PersonalRepository personalRepo) 
            : base(container, matchRepo, personalRepo)
        {
            _personalRepo.Updated += OnUpdate;
            _personalRepo.Refresh();
        }
        #endregion

        #region Functions
        private static Dictionary<int, int> CreateMonthlyDic(IEnumerable<Box> boxes)
        {
            return boxes
                .GroupBy(b => b.Created.Month)
                .OrderBy(m => m.Key)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.GetProhit()));
        }

        private static SeriesCollection CreateSeriesCollection(Dictionary<int, int> dic)
        {
            var prohitSeries = new ColumnSeries
            {
                Title = "수익",
                Values = new ChartValues<int>(dic.Values),
            };

            var collection = new SeriesCollection(prohitSeries);

            return collection;
        }

        private static string[] CreateMonths(Dictionary<int, int> dic)
        {
            return dic.Keys.Select(key => key.ToString()).ToArray();
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            if (!(sender is PersonalRepository personal))
                return;

            GageViewModel.Update(personal.Boxes);
        }
        #endregion
    }
}

using Jaywapp.Toasket.Model;
using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
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
        private List<Box> _boxes = new List<Box>();
        
        private ObservableAsPropertyHelper<List<Box>> _hittedBoxes;
        private ObservableAsPropertyHelper<double> _hazard;
        private ObservableAsPropertyHelper<int> _totalExpenditure;
        private ObservableAsPropertyHelper<int> _totalDividend;
        private ObservableAsPropertyHelper<int> _totalProhit;
        #endregion

        #region Properties
        public List<Box> Boxes
        {
            get => _boxes;
            set => this.RaiseAndSetIfChanged(ref _boxes, value);
        }

        public List<Box> HittedBoxes => _hittedBoxes.Value;
        public int TotalExpenditure => _totalExpenditure.Value;
        public int TotalDividend => _totalDividend.Value;
        public int TotalProhit => _totalProhit.Value;
        public double Hazard => _hazard.Value;
        #endregion

        #region Internal Field
        public AnalysisViewModel(IUnityContainer container, MatchRepository matchRepo, PersonalRepository personalRepo) 
            : base(container, matchRepo, personalRepo)
        {
            _personalRepo.Updated += OnUpdate;
            _personalRepo.Refresh();

            this.WhenAnyValue(x => x.Boxes)
                .Select(boxes => boxes.Where(b => b.IsHitted()).ToList())
                .ToProperty(this, x => x.HittedBoxes, out _hittedBoxes);

            this.WhenAnyValue(x => x.Boxes)
                .Where(b => b != null && b.Any())
                .Select(h=> h.Sum(b=> b.Money))
                .ToProperty(this, x => x.TotalExpenditure, out _totalExpenditure);

            this.WhenAnyValue(x => x.HittedBoxes)
                .Where(b => b != null && b.Any())
                .Select(h => h.Sum(b => b.GetReturnMoney()))
                .ToProperty(this, x => x.TotalDividend, out _totalDividend);

            this.WhenAnyValue(x => x.TotalExpenditure, x => x.TotalDividend)
                .Select(p => p.Item2 - p.Item1)
                .ToProperty(this, x => x.TotalProhit, out _totalProhit);

            this.WhenAnyValue(x => x.TotalProhit)
                .Select(p => CalculateHazard(TotalExpenditure, TotalProhit))
                .ToProperty(this, x => x.Hazard, out _hazard);
        }
        #endregion

        #region Functions
        private static double CalculateHazard(int expenditure, int prohit)
        {
            if (prohit >= 0)
                return 0;
            
            return Math.Abs(prohit / expenditure) * 100;
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            if (!(sender is PersonalRepository personal))
                return;

            Boxes = personal.Boxes;
        }
        #endregion
    }
}

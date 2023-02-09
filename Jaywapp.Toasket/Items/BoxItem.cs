using DynamicData;
using DynamicData.Binding;
using Jaywapp.Toasket.Helper;
using Jaywapp.Toasket.Interface;
using Jaywapp.Toasket.Model;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Jaywapp.Toasket.Items
{
    public class BoxItem : ReactiveObject, IAccount
    {
        #region Internal Field
        private int _expenditure = 0;
        private ObservableAsPropertyHelper<int> _income;
        private ObservableAsPropertyHelper<double> _ratio;
        #endregion

        #region Properties
        /// <summary>
        /// 원본 모델
        /// </summary>
        public Box Box { get; }

        /// <inheritdoc/>
        public int Expenditure
        {
            get => _expenditure;
            set => this.RaiseAndSetIfChanged(ref _expenditure, value);
        }

        /// <inheritdoc/>
        public int Income => _income.Value;

        /// <inheritdoc/>
        public int Profit => Income - Expenditure;

        /// <summary>
        /// 배당
        /// </summary>
        public double Ratio => _ratio.Value;

        /// <summary>
        /// 하위 매치 목록
        /// </summary>
        public ObservableCollection<MatchItem> Children { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="box"></param>
        public BoxItem(Box box)
        {
            Box = box;
            Expenditure = box.Expenditure;
            Children = box.Picks
                .Select(p => new MatchItem(p))
                .ToObservableCollection();

            this.WhenAnyValue(x => x.Expenditure)
                .BindTo(this, x => x.Box.Expenditure);

            this.WhenAnyValue(x => x.Expenditure)
                 .Select(p => Box.GetRatio())
                 .ToProperty(this, x => x.Ratio, out _ratio);

            Children.ToObservableChangeSet()
                .AutoRefresh(c => c.Pick)
                .Throttle(TimeSpan.FromSeconds(0.01))
                .Select(p => Box.GetRatio())
                .ToProperty(this, x => x.Ratio, out _ratio);

            this.WhenAnyValue(x => x.Expenditure, x => x.Ratio)
                .Select(p => Box.Income)
                .ToProperty(this, x => x.Income, out _income);
        }
        #endregion
    }
}

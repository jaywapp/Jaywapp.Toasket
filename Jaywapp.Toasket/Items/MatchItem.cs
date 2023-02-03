using Jaywapp.Toasket.Interface;
using Jaywapp.Toasket.Model;
using ReactiveUI;
using System;

namespace Jaywapp.Toasket.Items
{
    public class MatchItem : ReactiveObject, IMatch
    {
        #region Internal Field
        private Match _match;
        private eMatchResult _selectedResult;
        #endregion

        #region Properties
        public int No => _match.No;
        public string Home => _match.Home;
        public string Away => _match.Away;
        public DateTime Time => _match.Time;

        public eMatchResult SelectedResult
        {
            get => _selectedResult;
            set => this.RaiseAndSetIfChanged(ref _selectedResult, value);
        }

        public bool IsWinSelected
        {
            get => SelectedResult == eMatchResult.Win;
            set => SelectedResult = value ? eMatchResult.Win : eMatchResult.None;
        }

        public bool IsDrawSelected
        {
            get => SelectedResult == eMatchResult.Draw;
            set => SelectedResult = value ? eMatchResult.Draw : eMatchResult.None;
        }

        public bool IsLoseSelected
        {
            get => SelectedResult == eMatchResult.Lose;
            set => SelectedResult = value ? eMatchResult.Lose : eMatchResult.None;
        }

        public double Win => _match.Win;
        public double Draw => _match.Draw;
        public double Lose => _match.Lose;
        #endregion

        public MatchItem(Match match)
        {
            _match = match;

            this.WhenAnyValue(x => x.SelectedResult)
                .Subscribe(r=>
                {
                    this.RaisePropertyChanged(nameof(IsWinSelected));
                    this.RaisePropertyChanged(nameof(IsLoseSelected));
                    this.RaisePropertyChanged(nameof(IsDrawSelected));
                });
        }


        public double GetRatio()
        {
            switch (SelectedResult)
            {
                case eMatchResult.Win:  return Win;
                case eMatchResult.Draw: return Draw;
                case eMatchResult.Lose: return Lose;
                case eMatchResult.None: 
                default:
                    return 1;
            }
        }
    }
}

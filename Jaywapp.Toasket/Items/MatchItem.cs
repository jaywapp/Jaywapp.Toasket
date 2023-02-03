using Jaywapp.Toasket.Interface;
using Jaywapp.Toasket.Model;
using ReactiveUI;
using System;

namespace Jaywapp.Toasket.Items
{
    public class MatchItem : ReactiveObject
    {
        #region Internal Field
        private eMatchResult _selectedResult;
        #endregion

        #region Properties
        public Match Match { get; }
        
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
        #endregion

        public MatchItem(Match match)
        {
            Match = match;

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
                case eMatchResult.Win:  return Match.Win;
                case eMatchResult.Draw: return Match.Draw;
                case eMatchResult.Lose: return Match.Lose;
                case eMatchResult.None: 
                default:
                    return 1;
            }
        }

        public Pick ToPick() => new Pick(Match, SelectedResult, GetRatio());
    }
}

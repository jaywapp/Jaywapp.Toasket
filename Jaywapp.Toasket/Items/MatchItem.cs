using Jaywapp.Toasket.Model;
using ReactiveUI;
using System;

namespace Jaywapp.Toasket.Items
{
    public class MatchItem : ReactiveObject
    {
        #region Internal Field
        private ePick _pick;
        #endregion

        #region Properties
        public Match Match { get; }
        
        public ePick Pick
        {
            get => _pick;
            set => this.RaiseAndSetIfChanged(ref _pick, value);
        }

        public bool IsWinSelected
        {
            get => Pick == ePick.Win;
            set => Pick = value ? ePick.Win : ePick.None;
        }

        public bool IsDrawSelected
        {
            get => Pick == ePick.Draw;
            set => Pick = value ? ePick.Draw : ePick.None;
        }

        public bool IsLoseSelected
        {
            get => Pick == ePick.Lose;
            set => Pick = value ? ePick.Lose : ePick.None;
        }
        #endregion

        public MatchItem(Match match)
        {
            Match = match;
            Pick = match.Pick;

            this.WhenAnyValue(x => x.Pick).Subscribe(ChangePick);
        }

        private void ChangePick(ePick pick)
        {
            Match.Pick = pick;
            RaisePropertiesChanged();
        }

        private void RaisePropertiesChanged()
        {
            this.RaisePropertyChanged(nameof(IsWinSelected));
            this.RaisePropertyChanged(nameof(IsLoseSelected));
            this.RaisePropertyChanged(nameof(IsDrawSelected));
        }
    }
}

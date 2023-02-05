using Jaywapp.Toasket.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using AdonisUI.Controls;
using System.Windows;

namespace Jaywapp.Toasket.Items
{
    public class MatchItem : ReactiveObject
    {
        #region Internal Field
        private eMatchResult _pick;
        #endregion

        #region Properties
        public Match Match { get; }
        
        public eMatchResult Pick
        {
            get => _pick;
            set => this.RaiseAndSetIfChanged(ref _pick, value);
        }

        public bool IsWinSelected
        {
            get => Pick == eMatchResult.Win;
            set
            {
                if (Brothers.Any(b => b.Pick != eMatchResult.None))
                {
                    ShowMessage("이미 동일한 경기를 선택하셨습니다.");
                    return;
                }

                Pick = value ? eMatchResult.Win : eMatchResult.None;
            }
        }

        public bool IsDrawSelected
        {
            get => Pick == eMatchResult.Draw;
            set
            {
                if (Brothers.Any(b => b.Pick != eMatchResult.None))
                {
                    ShowMessage("이미 동일한 경기를 선택하셨습니다.");
                    return;
                }

                Pick = value ? eMatchResult.Draw : eMatchResult.None;
            }
        }

        public bool IsLoseSelected
        {
            get => Pick == eMatchResult.Lose;
            set
            {
                if (Brothers.Any(b => b.Pick != eMatchResult.None))
                {
                    ShowMessage("이미 동일한 경기를 선택하셨습니다.");
                    return;
                }

                Pick = value ? eMatchResult.Lose : eMatchResult.None;
            }
        }

        public Visibility DrawButtonVisibility { get; } = Visibility.Visible;

        public Visibility ResultVisibility { get; } = Visibility.Collapsed;

        public List<MatchItem> Brothers { get; set; } = new List<MatchItem>();
        #endregion

        public MatchItem(Match match)
        {
            Match = match;
            Pick = match.Pick;
            ResultVisibility = match.Result != eMatchResult.None ? Visibility.Visible : Visibility.Collapsed;

            if (match.Draw == 1)
                DrawButtonVisibility = Visibility.Collapsed;

            this.WhenAnyValue(x => x.Pick).Subscribe(ChangePick);
        }

        private void ChangePick(eMatchResult pick)
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

        private void ShowMessage(string msg)
        {
            var messageBox = new MessageBoxModel
            {
                Text = msg,
                Caption = "Info",
                Icon = AdonisUI.Controls.MessageBoxImage.Information,
                Buttons = new IMessageBoxButtonModel[]
                {
                    AdonisUI.Controls.MessageBoxButtons.Ok(),
                },
            };

            AdonisUI.Controls.MessageBox.Show(messageBox);
        }
    }
}

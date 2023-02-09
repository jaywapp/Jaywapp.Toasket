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
        /// <summary>
        /// 원본 모델
        /// </summary>
        public Match Match { get; }
        
        /// <summary>
        /// Match Pick
        /// </summary>
        public eMatchResult Pick
        {
            get => _pick;
            set => this.RaiseAndSetIfChanged(ref _pick, value);
        }

        /// <summary>
        /// 승 선택 여부
        /// </summary>
        public bool IsWinSelected
        {
            get => Pick == eMatchResult.Win;
            set => SetPick(value, eMatchResult.Win);
        }

        /// <summary>
        /// 무 선택 여부
        /// </summary>
        public bool IsDrawSelected
        {
            get => Pick == eMatchResult.Draw;
            set => SetPick(value, eMatchResult.Draw);
        }

        /// <summary>
        /// 패 선택 여부
        /// </summary>
        public bool IsLoseSelected
        {
            get => Pick == eMatchResult.Lose;
            set => SetPick(value, eMatchResult.Lose);
        }

        /// <summary>
        /// 승 버튼 Visibility
        /// </summary>
        public Visibility WinButtonVisibility { get; } = Visibility.Visible;
        /// <summary>
        /// 무 버튼 Visibility
        /// </summary>
        public Visibility DrawButtonVisibility { get; } = Visibility.Visible;
        /// <summary>
        /// 패 버튼 Visibility
        /// </summary>
        public Visibility LoseButtonVisibility { get; } = Visibility.Visible;
        /// <summary>
        /// 결과 Visibility
        /// </summary>
        public Visibility ResultVisibility { get; } = Visibility.Collapsed;
        /// <summary>
        /// 동일 경기 (다른 게임)
        /// </summary>
        public List<MatchItem> Brothers { get; set; } = new List<MatchItem>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="match"></param>
        public MatchItem(Match match)
        {
            Match = match;
            Pick = match.Pick;
            
            ResultVisibility = GetVisibility(match.Result);
            DrawButtonVisibility = GetVisibility(match.Draw);
            WinButtonVisibility = GetVisibility(match.Win);
            LoseButtonVisibility = GetVisibility(match.Lose);

            this.WhenAnyValue(x => x.Pick).Subscribe(ChangePick);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Match의 예상 결과를 Pick합니다.
        /// </summary>
        /// <param name="isSelected"></param>
        /// <param name="pick"></param>
        private void SetPick(bool isSelected, eMatchResult pick)
        {
            if (Brothers.Any(b => b.Pick != eMatchResult.None))
                ShowMessage("이미 동일한 경기를 선택하셨습니다.");
            else 
                Pick = isSelected ? pick : eMatchResult.None;
        }

        /// <summary>
        /// 모델의 Pick을 변경합니다.
        /// </summary>
        /// <param name="pick"></param>
        private void ChangePick(eMatchResult pick)
        {
            Match.Pick = pick;
            RaisePropertiesChanged();
        }

        /// <summary>
        /// Property Changed 이벤트 발생
        /// </summary>
        private void RaisePropertiesChanged()
        {
            this.RaisePropertyChanged(nameof(IsWinSelected));
            this.RaisePropertyChanged(nameof(IsLoseSelected));
            this.RaisePropertyChanged(nameof(IsDrawSelected));
        }

        /// <summary>
        /// 메세지 박스를 통해 <paramref name="msg"/>를 출력합니다.
        /// </summary>
        /// <param name="msg"></param>
        private void ShowMessage(string msg)
        {
            var messageBox = new MessageBoxModel
            {
                Text = msg,
                Caption = "Info",
                Icon = AdonisUI.Controls.MessageBoxImage.Information,
                Buttons = new IMessageBoxButtonModel[] { MessageBoxButtons.Ok(), },
            };

            AdonisUI.Controls.MessageBox.Show(messageBox);
        }

        /// <summary>
        /// <paramref name="result"/>에 따라 <see cref="Visibility"/>를 출력합니다.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static Visibility GetVisibility(eMatchResult result) 
            => result != eMatchResult.None ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// <paramref name="ratio"/>에 따라 <see cref="Visibility"/>를 출력합니다.
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        private static Visibility GetVisibility(double ratio) 
            => ratio != 1 ? Visibility.Visible : Visibility.Collapsed;
        #endregion
    }
}

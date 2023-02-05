using Jaywapp.Toasket.Model;
using Jaywapp.Toasket.Repository;
using Jaywapp.Toasket.View.Base;
using Microsoft.Practices.Unity;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace Jaywapp.Toasket.View.Tab
{
    public class AnalysisViewModel : ContainableReactiveObject
    {
        private List<Box> _boxes = new List<Box>();

        public List<Box> Boxes
        {
            get => _boxes;
            set => this.RaiseAndSetIfChanged(ref _boxes, value);
        }

        public AnalysisViewModel(IUnityContainer container, MatchRepository matchRepo, PersonalRepository personalRepo) 
            : base(container, matchRepo, personalRepo)
        {
            _personalRepo.Updated += OnUpdate;
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            if (!(sender is PersonalRepository personal))
                return;

            Boxes = personal.Boxes;
        }
    }
}

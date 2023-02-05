using Jaywapp.Toasket.Repository;
using Microsoft.Practices.Unity;
using ReactiveUI;
using System;

namespace Jaywapp.Toasket.View.Base
{
    public abstract class ContainableReactiveObject : ReactiveObject
    {
        protected readonly IUnityContainer _container;
        protected readonly MatchRepository _matchRepo;
        protected readonly PersonalRepository _personalRepo;

        public ContainableReactiveObject(
            IUnityContainer container, 
            MatchRepository matchRepo,
            PersonalRepository personalRepo)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _matchRepo = matchRepo ?? throw new ArgumentNullException(nameof(matchRepo));
            _personalRepo = personalRepo ?? throw new ArgumentNullException(nameof(personalRepo));
        }
    }
}

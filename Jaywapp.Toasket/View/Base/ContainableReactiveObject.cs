using Jaywapp.Toasket.Repository;
using Microsoft.Practices.Unity;
using ReactiveUI;
using System;

namespace Jaywapp.Toasket.View.Base
{
    public abstract class ContainableReactiveObject : ReactiveObject
    {
        protected readonly IUnityContainer _container;
        protected readonly MatchRepository _dataRepository;
        protected readonly PersonalRepository _personalRepository;

        public ContainableReactiveObject(
            IUnityContainer container, 
            MatchRepository dataRepository,
            PersonalRepository personalRepository)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
            _personalRepository = personalRepository ?? throw new ArgumentNullException(nameof(personalRepository));
        }
    }
}

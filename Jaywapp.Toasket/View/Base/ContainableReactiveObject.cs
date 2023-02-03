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

        public ContainableReactiveObject(IUnityContainer container, MatchRepository dataRepository)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }
    }
}

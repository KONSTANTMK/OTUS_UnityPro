using System;
using Unity.VisualScripting;

namespace Homework.EventBus
{
    public abstract class BaseEventHandler<TEvent> : IInitializable, IDisposable
    {
        protected readonly EventBus EventBus;

        public BaseEventHandler(EventBus eventBus)
        {
            EventBus = eventBus;
        }
        
        void IInitializable.Initialize()
        {
            EventBus.Subscribe<TEvent>(OnEventInvoked);
        }

        void IDisposable.Dispose()
        {
            EventBus.Unsubscribe<TEvent>(OnEventInvoked);
        }

        protected abstract void OnEventInvoked(TEvent evt);
    }
}
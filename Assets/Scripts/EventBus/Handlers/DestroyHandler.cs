namespace Homework.EventBus
{
    public sealed class DestroyHandler: BaseEventHandler<DestroyEvent>
    {
        private readonly LevelMap _levelMap;


        public DestroyHandler(EventBus eventBus, LevelMap levelMap) : base(eventBus)
        {
            _levelMap = levelMap;
        }

        protected override void OnEventInvoked(DestroyEvent evt)
        {
            if (evt.Entity.TryGet(out DeathComponent deathComponent))
            {
                deathComponent.Die();
            }

            var coordinates = evt.Entity.Get<CoordinatesComponent>();
            _levelMap.Entities.RemoveEntity(coordinates.Value);
            
            // if (evt.Entity.TryGet(out DestroyComponent destroyComponent))
            // {
            //     destroyComponent.Destroy();
            // }
        }
    }
    
    public sealed class DestroyVisualHandler: BaseEventHandler<DestroyEvent>
    {
        private VisualPipeline _visualPipeline;
        
        public DestroyVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void OnEventInvoked(DestroyEvent evt)
        {
            _visualPipeline.AddTask(new DestroyVisualTask(evt.Entity));
        }
    }

    public class DestroyVisualTask : EventTask
    {
        public IEntity Entity;

        public DestroyVisualTask(IEntity entity)
        {
            Entity = entity;
        }

        protected override void OnStart()
        {
            var transformComponent = Entity.Get<TransformComponent>();
            Entity.Get<TransformComponent>().Value.DOScale(0f, 1f)
                .OnComplete(Complete).SetLink(transformComponent.Value.GameObject);
        }
    }
}
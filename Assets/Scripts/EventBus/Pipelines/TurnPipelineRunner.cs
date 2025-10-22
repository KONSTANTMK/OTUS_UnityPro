using Sirenix.OdinInspector;
using Zenject;

namespace Homework.EventBus
{
    [ShowInInspector]
    public class TurnPipelineRunner
    {
        private TurnPipeline _turnPipeline;
        [Inject]
        public void Construct(TurnPipeline turnPipeline, DiContainer diContainer)
        {
            _turnPipeline = turnPipeline;
            InstallPipeline(diContainer);

            _turnPipeline.OnCompleted += OnPipelineCompleted;
        }

        private void Start()
        {
            RunPipeline();
        }
        
        private void OnPipelineCompleted()
        {
            RunPipeline();
        }

        private void InstallPipeline(DiContainer diContainer)
        {
            _turnPipeline.AddTask(new StartTurnTask());

            var playerInputTask = new PlayerInputTask();
            diContainer.Inject(playerInputTask); 
            _turnPipeline.AddTask(playerInputTask);

            var startVisualPipelineTask = new VisualStartPipelineTask();
            diContainer.Inject(startVisualPipelineTask);
            _turnPipeline.AddTask(startVisualPipelineTask);
            
            _turnPipeline.AddTask(new FinishTurnTask());
        }

        [Button]
        public void RunPipeline()
        {
            _turnPipeline.Reset();
            _turnPipeline.Run();
        }
    }
}
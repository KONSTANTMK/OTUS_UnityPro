using UnityEngine;

namespace Homework.EventBus
{
    public class VisualStartPipelineTask : EventTask
    {
        private VisualPipeline _visualPipeline;

        public void Construct(VisualPipeline visualPipeline)
        {
            _visualPipeline = visualPipeline;
        }
        
        protected override void OnStart()
        {
            Debug.Log("Start VisualStartPipelineTask");
            _visualPipeline.OnCompleted += OnPipelineCompleted;
            _visualPipeline.Reset();
            _visualPipeline.Run();
        }

        private void OnPipelineCompleted()
        {
            _visualPipeline.OnCompleted -= OnPipelineCompleted;
            _visualPipeline.ClearTasks();
            Complete();
        }
    }
}
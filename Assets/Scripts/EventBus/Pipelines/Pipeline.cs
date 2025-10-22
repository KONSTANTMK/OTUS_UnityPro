using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Homework.EventBus
{
    public abstract class Pipeline
    {
        
        public event Action OnCompleted;
        [ShowInInspector]
        public List<EventTask> EventTasks = new ();
        
        [ShowInInspector]
        private int _index = -1;
        
        public void Run()
        {
            _index++;
            if (_index >= EventTasks.Count)
            {
               Complete();
               return;
            }

            var task = EventTasks[_index];
            task.Run(Run);
        }

        private void Complete()
        {
           Debug.Log("Complete pipeline"); 
           OnCompleted?.Invoke();
        }

        public void AddTask(EventTask eventTask)
        {
            EventTasks.Add(eventTask);
        }

        public void ClearTasks()
        {
            EventTasks.Clear();
        }
        
        public void Reset()
        {
          _index = -1;
        }
    }
}
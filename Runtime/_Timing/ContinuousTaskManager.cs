using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Timing
{
    public class ContinuousTaskManager : GameSystem<ContinuousTaskManager> 
    {
        private List<ContinuousTask> tasks = new List<ContinuousTask>();

        private void Update()
        {
            for (int i = tasks.Count - 1; i >= 0; i--)
            {
                ContinuousTask task = tasks[i];
                if ((task.CanContinueTask() && task.PerformTask()) || task.isComplete)
                {
                    task.CompleteTask();
                    tasks.RemoveAt(i);
                }
            }
        }

        public void Enqueue(ContinuousTask task) 
        { 
            if (!tasks.Contains(task))
                tasks.Add(task);
        }
    }
}
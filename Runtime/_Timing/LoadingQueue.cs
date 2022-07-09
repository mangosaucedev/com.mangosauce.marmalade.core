using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Timing
{
    public class LoadingQueue : GameSystem<LoadingQueue>
    {
        private List<LoadingTask> queue = new List<LoadingTask>();
        private LoadingTask currentTask;

        public LoadingTask CurrentTask
        {
            get
            {
                if (queue.Count == 0)
                    return null;
                return queue[queue.Count - 1];
            }
        }

        public bool IsLoading => CurrentTask != null;

        public bool Enqueue(LoadingTask loadingTask)
        {
            if (queue.Contains(loadingTask))
                return false;
            loadingTask.OnCompleteTask(() => queue.Remove(loadingTask));
            queue.Insert(0, loadingTask);
            return true;
        }

        private void Update()
        {
            if (queue.Count == 0)
                return;
            if (currentTask == null || currentTask.isComplete)
                NextTask();
        }

        private void NextTask()
        { 
            currentTask = CurrentTask;
            if (currentTask != null)
                Resources.Get<ContinuousTaskManager>().Enqueue(CurrentTask);
        }
    }
}

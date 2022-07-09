using System;
using UnityEngine;

namespace Marmalade.Timing
{
    public class ContinuousTask
    {
        public bool isComplete;

        protected Func<bool> onPerform;
        protected Func<bool> canContinue;
        protected Action onComplete;
        protected float timeLastPerformed;
        protected float delay;

        public ContinuousTask(Func<bool> onPerform, Func<bool> canContinue = null, Action onComplete = null)
        {
            this.onPerform = onPerform;
            this.canContinue = canContinue;
            this.onComplete = onComplete;
        }

        public bool CanContinueTask()
        {
            if (delay > 0 && Time.time - timeLastPerformed + float.Epsilon < delay)
                return false;
            if (canContinue == null)
                return true;
            return canContinue();
        }

        public bool PerformTask()
        {
            timeLastPerformed = Time.time;
            if (onPerform == null)
                return true;
            return onPerform();
        }

        public void CompleteTask()
        {
            isComplete = true;
            onComplete?.Invoke();
        }

        public ContinuousTask WithDelay(float delayInSeconds)
        {
            delay = delayInSeconds;
            return this;
        }
    }
}
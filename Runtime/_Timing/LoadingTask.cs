using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Timing
{
    public class LoadingTask : ContinuousTask
    {
        private Func<float> getProgress;
        private string message;

        public LoadingTask(Func<bool> onPerform, Func<bool> canContinue = null) :
            base(onPerform, canContinue)
        {

        }

        public LoadingTask OnCompleteTask(Action onComplete)
        {
            if (this.onComplete == null)
            {
                this.onComplete = onComplete;
                return this;
            }
            this.onComplete += onComplete;
            return this;
        }

        public LoadingTask WithProgressGetter(Func<float> getProgress)
        {
            this.getProgress = getProgress;
            return this;
        }

        public LoadingTask WithMessage(string message)
        {
            this.message = message;
            return this;
        }

        public float GetProgress()
        {
            if (getProgress == null)
                return 0.5f;
            return Mathf.Clamp01(getProgress());
        }

        public string GetMessage()
        {
            if (string.IsNullOrEmpty(message))
                return "...";
            return message;
        }
    }
}

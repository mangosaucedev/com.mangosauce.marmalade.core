using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public class TransitionFromAny<T> where T : IState<T>
    {
        public T goTo;

        protected Func<bool> transition;

        public TransitionFromAny(Func<bool> transition, T goTo)
        {
            this.transition = transition;
            this.goTo = goTo;
        }

        public bool CanTransition()
        {
            if (transition != null)
                return transition.Invoke();
            return false;
        }
    }
}

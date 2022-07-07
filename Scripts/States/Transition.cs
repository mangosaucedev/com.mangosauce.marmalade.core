using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public class Transition<T> : TransitionFromAny<T> where T : IState<T>
    {
        public readonly T state;

        public Transition(T state, Func<bool> transition, T goTo) : base(transition, goTo)
        {
            this.state = state;
        }
    }
}

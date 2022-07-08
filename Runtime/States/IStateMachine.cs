using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public interface IStateMachine<T>: IResource, IEquatable<IStateMachine<T>> where T : IState<T>
    {

        T State { get; set; }

        HashSet<Transition<T>> Transitions { get; }

        HashSet<TransitionFromAny<T>> TransitionsFromAny { get; }

        void Update();

        void FixedUpdate();

        T AddState(T state, bool goTo);

        void GoToState(T state);
    }
}

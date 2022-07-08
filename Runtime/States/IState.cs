using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public interface IState<T> : IStateMachine<T> where T : IState<T>
    {
        void Start();

        void End();
    }
}

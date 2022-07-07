using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public class GameSystem<T> : MonoBehaviour, IResource where T : GameSystem<T>
    {
        protected Guid guid = Guid.NewGuid();

        public Guid Guid => guid;

        protected virtual void Awake()
        {
            Resources.Register<T>(this);
        }
    }
}

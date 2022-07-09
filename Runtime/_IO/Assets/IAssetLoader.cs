using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.IO
{
    public interface IAssetLoader
    {
        string Message { get; }

        public bool LoadAssets();

        public bool IsValid();

        public float GetProgress();
    }
}

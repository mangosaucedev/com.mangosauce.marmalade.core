using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.IO
{
    public interface IAssetLoader
    {
        public IEnumerator LoadAssets();
    }
}

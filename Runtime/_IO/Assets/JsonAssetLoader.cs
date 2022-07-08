using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Marmalade.IO
{
    public class JsonAssetLoader<T> : IAssetLoader where T : IAsset
    {
        public IEnumerator LoadAssets()
        {
            yield return null;
        }
    }
}

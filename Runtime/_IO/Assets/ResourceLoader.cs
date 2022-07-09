using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.IO
{
    public abstract class ResourceLoader<T> : IAssetLoader where T : Object
    {
        protected T[] resources;

        private int resourceIndex;
        private int resourcesLoaded;
        private bool encounteredError;

        public abstract string Message { get; }

        public ResourceLoader()
        {
            resources = UnityEngine.Resources.LoadAll<T>("");
        }

        public bool LoadAssets()
        {
            if (resources != null && resources.Length > 0)
            {
                try
                {
                    T resource = resources[resourceIndex];
                    Asset<T> asset = new Asset<T>(resource);
                    asset.Name = resource.name;
                    Assets.Add(resource.name, asset);
                    resourcesLoaded++;
                }
                catch
                {
                    throw new AssetLoadException(typeof(T), "Resources/");
                }

                resourceIndex++;
            }

            if (!Mathf.Approximately(GetProgress(), 1f))
                return false;

            DebugLogger.Log("Finished importing " + resourcesLoaded + " " + typeof(T).Name + " resources.");
            return true;
        }

        public float GetProgress()
        {
            int max = resources != null ? resources.Length : 1;
            if (max == 0)
                return 1f;
            return (float) resourceIndex / max;
        }

        public bool IsValid() => !encounteredError;
    }
}

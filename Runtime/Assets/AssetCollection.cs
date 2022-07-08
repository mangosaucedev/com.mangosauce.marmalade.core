using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public class AssetCollection
    {
        public Dictionary<string, IAsset> collection =
            new Dictionary<string, IAsset>();
        public HashSet<string> assetNames = new HashSet<string>();

        public T Get<T>(string name) where T : IAsset
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception($"Cannot retrieve asset with no name!");
            if (collection.TryGetValue(name, out IAsset obj))
                return (T) obj;
            throw new Exception($"No {typeof(T).Name} asset exists with name {name}!");
        }

        public bool Exists(string name) => collection.ContainsKey(name);

        public void Add(string name, IAsset obj)
        {
            collection[name] = obj;
            assetNames.Add(name);
        }

        public List<T> FindAll<T>(string nameContains) where T : IAsset
        {
            List<T> assets = new List<T>();
            foreach (string name in assetNames)
            {
                if (name.Contains(nameContains))
                {
                    T asset = (T) collection[name];
                    assets.Add(asset);
                }
            }
            return assets;
        }
    }
}
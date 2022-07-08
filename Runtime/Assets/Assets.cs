using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Marmalade.IO;

namespace Marmalade
{
    public class Assets : GameSystem<Assets>
    {
        private static Dictionary<Type, AssetCollection> collections =
            new Dictionary<Type, AssetCollection>();

        private IEnumerator Start()
        {
            yield return null;
        }

        public static T Get<T>(string name) where T : IAsset
        {
            Type type = typeof(T);
            AssetCollection collection = GetAssetCollection(type);
            return collection.Get<T>(name);
        }

        public static bool Exists<T>(string name) where T : IAsset
        {
            Type type = typeof(T);
            AssetCollection collection = GetAssetCollection(type);
            return collection.Exists(name);
        }

        public static void Add<T>(string name, T obj) where T : IAsset
        {
            Type type = typeof(T);
            AssetCollection collection = GetAssetCollection(type);
            collection.Add(name, obj);
        }

        private static AssetCollection GetAssetCollection(Type type)
        {
            if (collections.TryGetValue(type, out AssetCollection collection))
                return collection;
            collection = new AssetCollection();
            collections[type] = collection;
            return collection;
        }

        public static List<T> FindAll<T>(string nameContains = "") where T : IAsset
        {
            Type type = typeof(T);
            AssetCollection assetCollection = GetAssetCollection(type);
            return assetCollection.FindAll<T>(nameContains);
        }
    }
}

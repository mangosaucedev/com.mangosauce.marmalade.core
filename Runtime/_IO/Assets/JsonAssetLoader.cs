using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace Marmalade.IO
{
    public abstract class JsonAssetLoader<T> : IAssetLoader where T : IAsset
    {
        private static JsonSerializerSettings settings = new JsonSerializerSettings 
        { 
            Culture = CultureInfo.CurrentCulture,
            NullValueHandling = NullValueHandling.Ignore,
            StringEscapeHandling = StringEscapeHandling.EscapeHtml
        };

        protected List<string> paths;
        protected int fileIndex;
        protected int deserializedFiles;
        protected bool error;

        public abstract string Message { get; }

        protected abstract string Prefix { get; }

        protected abstract string Extension { get; }

        public JsonAssetLoader()
        {
            paths = (from path in Directory.GetFiles(Application.streamingAssetsPath, string.Format(@"^{0}_[a-zA-Z0-9.]*.{1}$", Prefix, Extension), SearchOption.AllDirectories)
                     where !path.EndsWith(".meta")
                     select path).ToList();
        }

        public bool LoadAssets()
        {
            if (paths != null && paths.Count > 0)
            {
                string path = paths[fileIndex];

                try
                {
                    string name = Path.GetFileNameWithoutExtension(path);
                    string json = File.ReadAllText(path);
                    T obj = JsonConvert.DeserializeObject<T>(json, settings);
                    obj.Name = Path.GetFileNameWithoutExtension(path);
                    obj.Path = path;
                    AddAsset(name, obj);
                    deserializedFiles++;
                }
                catch
                {
                    throw new AssetLoadException(typeof(T), path);
                }

                fileIndex++;
            }

            if (!Mathf.Approximately(GetProgress(), 1f))
                return false;

            DebugLogger.Log("Successfully deserialized " + deserializedFiles + " " + typeof(T).Name + "(s).");
            return true;
        }

        public virtual void AddAsset(string name, T obj)
        {
            Assets.Add(name, obj);
        }

        public float GetProgress()
        {
            int max = paths != null ? paths.Count : 1;
            if (max == 0)
                return 1f;
            return (float) fileIndex / max;
        }

        public bool IsValid() => !error;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public class Asset<T> : IAsset
    {
        private T packedAsset;

        public string Name { get; set; }

        public string Path { get; set; }

        public bool Unpackable => false;

        public T Contents
        {
            get => packedAsset;
            set => packedAsset = packedAsset == null ? value : packedAsset;
        }

        public void UnpackAssets()
        {

        }
    }
}

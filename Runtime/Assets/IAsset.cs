using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public interface IAsset
    {
        string Name { get; set; }

        string Path { get; set; }

        bool Unpackable { get; }

        void UnpackAssets();
    }
}

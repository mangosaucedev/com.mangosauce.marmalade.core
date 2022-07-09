using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public class AssetLoadException : Exception
    {
        public Type type;
        public string path;

        public AssetLoadException(Type type, string path) : 
            base(message: $"Could not load {type.Name} asset from path {path}.")
        {
            this.type = type;
            this.path = path;
        }
    }
}

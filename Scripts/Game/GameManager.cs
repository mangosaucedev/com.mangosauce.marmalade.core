using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Marmalade
{
    [AddComponentMenu("FpCore/Logic/GameManager")]
    public class GameManager : GameSystem<GameManager>
    {
#if UNITY_WEBPLAYER
        private const string QUIT_URL = "http://google.com";
#endif
    }
}
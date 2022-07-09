using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using Marmalade;
using Marmalade.Inputs;
using Marmalade.Timing;
using Marmalade.UI;
using Resources = Marmalade.Resources;

namespace Marmalade.Editors
{
    public static class LogicMenuItems
    {
        private static Dictionary<Type, (GameObject gameObject, Component instance)> managers = 
            new Dictionary<Type, (GameObject gameObject, Component instance)>();

        [MenuItem("Marmalade/Logic/Create Game Logic Managers")]
        public static void CreateGameLogicManagers()
        {
            GameObject root = CreateManager("Game Manager", Selection.activeTransform, typeof(GameManager));
            Transform parent = root.transform;

            CreateManager("Assets", parent, typeof(Assets));
            CreateManager("Gamestate Machine", parent, typeof(GamestateMachine));
            CreateManager("Input Manager", parent, typeof(InputManager));
            CreateManager("Resources", parent, typeof(Resources));
            
            Transform timingParent = CreateManager("--- Timing --- ", parent).transform;
            CreateManager("Continuous Task Manager", timingParent, typeof(ContinuousTaskManager));
            CreateManager("Loading Queue", timingParent, typeof(LoadingQueue));

            Transform uiParent = CreateManager("--- UI --- ", parent).transform;
            CreateManager("Event System", uiParent, typeof(EventSystem));
            CreateManager("View Manager", uiParent, typeof(ViewManager));

            Undo.RegisterCreatedObjectUndo(root, "Game Logic Manager instantiation");
        }

        private static GameObject CreateManager(string name, Transform parent, params Type[] components)
        {
            GameObject gameObject = new GameObject(name, components);
            gameObject.transform.SetParent(parent);

            foreach (Type type in components)
                managers[type] = (gameObject, gameObject.GetComponent(type));

            return gameObject;
        }
    }
}

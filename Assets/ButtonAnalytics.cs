using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace GameCore
{
    [RequireComponent (typeof (Button))]
    public class ButtonAnalytics : MonoBehaviour
    {
        private Button _button = null;
        public Button button { get { if (_button == null) _button = GetComponent<Button> (); return _button; } }

        [SerializeField] string path;

        [ContextMenu ("ValidatePath")]
        private void ValidatePath ()
        {
            if (path.IsNullOrEmpty ())
            {
                var parent = transform.parent;
                Stack<string> stack = new Stack<string> ();

                stack.Push (name);
                // Debug.LogFormat ("stack.Log() = {0}", stack.Log ());
                while (parent)
                {
                    stack.Push (parent.name);
                    // Debug.LogFormat ("stack.Log() = {0}", stack.Log ());
                    parent = parent.parent;
                }

                // Debug.LogFormat ("stack.Count = {0}", stack.Count);
                int count = stack.Count;
                // Debug.LogFormat ("count = {0}", count);
                for (int i = 0; i < count; i++)
                {
                    string pop = stack.Pop ();
                    // Debug.LogFormat ("pop = {0}", pop);
                    path += $"/{pop}";
                    // Debug.LogFormat ("path = {0}", path);
                }
            }
        }

        void Start ()
        {

            ValidatePath ();
            button.onClick.AddListener (Send);
        }

        private void Send ()
        {
            Analytics.CustomEvent ("Button", new Dictionary<string, object>
            { { "path", path }
            });
        }
        public bool Check (string _path)
        {
            return _path == path;
        }

        [SerializeField] string FindByPath;

        [ContextMenu ("Find")]
        void Find ()
        {
            var objs = GameObject.FindObjectsOfType (typeof (ButtonAnalytics));
            foreach (ButtonAnalytics obj in objs)
            {
                if (obj.Check (FindByPath))
                {
                    obj.SelectMeInEditor ();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    public class SaveData : SingletonData<SaveData>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/SaveData")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void ResetDefault ()
        {
            foreach (var save in Saves)
                save.ResetDefault ();
        }
        public override void InitOnCreate () { }
#endif
        public List<ASavableVariable> Saves;

        Saver _saver = null;
        Saver saver
        {
            get
            {
                if (_saver == null)
                {
                    _saver = GameObject.FindObjectOfType<Saver> ();
                    if (_saver == null)
                    {
                        _saver = new GameObject ("Saver").AddComponent<Saver> ();
                        DontDestroyOnLoad (saver.gameObject);
                    }
                }
                return _saver;
            }
        }
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        static void SubscrideOneditorChangePlayingSatate ()
        {
            UnityEditor.EditorApplication.playModeStateChanged -= playmodeStateChanged;
            UnityEditor.EditorApplication.playModeStateChanged += playmodeStateChanged;
        }

        private static void playmodeStateChanged (UnityEditor.PlayModeStateChange state)
        {
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                instance.ResetLoaded ();
            }
        }
#endif
        public void CheckSaver ()
        {
            if (_saver) return;
            _saver = saver;
        }

        public bool Contains (ASavableVariable savable)
        {
            if (Saves == null) Saves = new List<ASavableVariable> ();
            return Saves.Contains (savable);
        }
        public void Add (ASavableVariable savable)
        {
            if (Saves == null) Saves = new List<ASavableVariable> ();
            Saves.Add (savable);
            this.Save ();
        }
        public void Remove (ASavableVariable savable)
        {
            if (Saves == null)
            {
                Saves = new List<ASavableVariable> ();
                return;
            }
            Saves.Remove (savable);
            this.Save ();
        }

        public void SaveAll ()
        {
            foreach (var save in Saves)
            {
                save.SaveValue ();
            }
        }

#if UNITY_EDITOR
        public void ResetLoaded ()
        {
            foreach (var save in Saves)
            {
                save.ResetLoaded ();
            }
        }

        [ContextMenu ("CleatMissingReference")]
        void ClearMissingReference ()
        {
            Saves.RemoveAll (item => item == null);
        }
#endif
    }
}

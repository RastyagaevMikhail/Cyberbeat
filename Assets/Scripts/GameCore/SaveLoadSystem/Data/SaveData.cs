using Sirenix.OdinInspector;
using Sirenix.Serialization;

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

        }
        public override void InitOnCreate () { }
#endif

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

        [SerializeField] List<ISavableVariable> Saves;
        public bool Loaded = false;

        public bool Contains (ISavableVariable savable)
        {
            if (Saves == null) Saves = new List<ISavableVariable> ();
            return Saves.Contains (savable);
        }
        public void Add (ISavableVariable savable)
        {
            if (Saves == null) Saves = new List<ISavableVariable> ();
            Saves.Add (savable);
            this.Save ();
        }
        public void Remove (ISavableVariable savable)
        {
            if (Saves == null)
            {
                Saves = new List<ISavableVariable> ();
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
        public void LoadAll ()
        {
            Loaded = true;
            foreach (var save in Saves)
            {
                save.LoadValue ();
            }
        }

#if UNITY_EDITOR
        public void ResetLoaded ()
        {
            Loaded = false;
            foreach (var save in Saves)
            {
                save.ResetLoaded ();
            }
        }
#endif
    }
}

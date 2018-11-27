using Sirenix.OdinInspector;
using Sirenix.Serialization;

using System;

using UnityEngine;

namespace GameCore
{
    [System.Serializable]
    public abstract class SavableVariable<T> : SerializedScriptableObject, ISavableVariable
    {
        [Multiline]
        public string DeveloperDescription = "";
        public string categoryTag = "Default";
        public string CategoryTag { get { return categoryTag; } }
        protected SaveData saveData { get { return SaveData.instance; } }

        [ShowInInspector]
        public bool isSavable
        {
            get { return saveData.Contains (this); }
            set
            {
                bool contains = saveData.Contains (this);
                if (value && !contains)
                {
                    saveData.Add (this);
#if UNITY_EDITOR
                    UnityEditor.Selection.activeObject = saveData;
#endif
                }
                else if (!value && contains) saveData.Remove (this);
            }
        }
#if UNITY_EDITOR
        private void SetValue ()
        {
            Value = _value;
            SaveValue();
        }

        [OnValueChanged ("SetValue")]
#endif
        [SerializeField]
        protected T _value;

        [NonSerialized]
        public Action<T> OnValueChanged = (o) => { };
        public bool Loaded = false;
        public virtual T Value
        {
            get
            {
                if (isSavable && !Loaded && Application.isPlaying)
                {
                    // Debug.LogFormat (this,"Get {1} = {0}", _value, name);
                    LoadValue ();
                }
                return _value;
            }
            set
            {
                _value = value;
                if (OnValueChanged != null)
                    OnValueChanged (_value);
                if (isSavable)
                {
                    if (Application.isPlaying)
                        saveData.CheckSaver ();
                    else
                        SaveValue ();
                }
            }
        }

        [Button]
        public abstract void SaveValue ();
        public virtual void LoadValue ()
        {
            Loaded = true;
        }
#if UNITY_EDITOR
        public void CreateAsset (string path = "")
        {
            if (path == "") path = "Assets/Data/Variables/{0}/{1}.asset".AsFormat (GetType ().Name, name);

            Tools.CreateAsset (this, path);

        }
        public void ResetLoaded ()
        {
            Loaded = false;
            UnityEditor.EditorUtility.SetDirty (this);
        }
#endif

    }
}

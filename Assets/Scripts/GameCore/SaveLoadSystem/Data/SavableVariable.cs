using System;

using UnityEngine;

namespace GameCore
{
    [System.Serializable]
    public abstract class SavableVariable<TValue> : ASavableVariable
    {
        [Multiline]
        public string DeveloperDescription = "";
        public string categoryTag = "Default";
        public override string CategoryTag { get { return categoryTag; } set { categoryTag = value; } }
        protected SaveData saveData { get { return SaveData.instance; } }

        public override bool isSavable
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

        [SerializeField]
        protected TValue _value;
        [SerializeField]
        protected TValue DefaultValue;
        [SerializeField]
        protected bool ResetByDefault;
        [ContextMenu ("Reset Default")]
        public override void ResetDefault ()
        {
            if (ResetByDefault)
                Value = DefaultValue;
        }
        public Action<TValue> OnValueChanged = (o) => { };
        public bool Loaded = false;
        public virtual TValue Value
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

        [ContextMenu ("Save Value")]
        public override void SaveValue () { }

        [ContextMenu ("Load Value")]
        public override void LoadValue ()
        {
            if (Application.isPlaying)
                Loaded = true;
        }

#if UNITY_EDITOR
        public override void CreateAsset (string path = "")
        {
            if (path == "") path = "Assets/Data/Variables/{0}/{1}.asset".AsFormat (GetType ().Name, name);

            Tools.CreateAsset (this, path);

        }
        public override void ResetLoaded ()
        {
            Loaded = false;
            UnityEditor.EditorUtility.SetDirty (this);
        }

        [ContextMenu ("Show Paths")] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }
#endif

    }
}

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
        [SerializeField, HideInInspector] SaveData saveData;
        private void OnValidate ()
        {
            if (!saveData)
                saveData = Resources.Load<SaveData> ("Data/SaveData");
        }

#if UNITY_EDITOR
        public override void SetSavable (bool value)
        {
            isSavable = value;
            this.Save ();
        }
#endif

        [SerializeField] bool isSavable;
        public override bool IsSavable => isSavable;

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
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public Action<TValue> OnValueChanged = (o) => { };
        public bool Loaded = false;
       
        public virtual TValue Value
        {
            get
            {
                if (IsSavable && !Loaded && Application.isPlaying)
                {
                    LoadValue ();
                }
                return _value;
            }
            set
            {
                _value = value;
                if (OnValueChanged != null)
                    OnValueChanged (_value);
                if (IsSavable)
                {
                    if (Application.isPlaying)
                        saveData.CheckSaver ();
                    else
                        SaveValue ();
                }
            }
        }

        [ContextMenu ("Save Value")]
        public override void SaveValue () { Debug.Log ($"SavableVariable.SaveValue.{name}"); }

        [ContextMenu ("Load Value")]
        public override void LoadValue ()
        {
            if (Application.isPlaying)
                Loaded = true;
        }

        public override void CreateAsset (string path = "", bool IsSaveble = false)
        {
#if UNITY_EDITOR
            if (string.IsNullOrEmpty (path))
                path = $"Assets/Data/Variables/{GetType ().Name}/{name}.asset";

            Tools.CreateAsset (this, path);
            isSavable = IsSaveble;
#endif
        }
#if UNITY_EDITOR
        public override void ResetLoaded ()
        {
            Loaded = false;
            // this.Save ();
        }
#endif

    }
}

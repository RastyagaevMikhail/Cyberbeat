using Sirenix.OdinInspector;

using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (fileName = "BoolVariable", menuName = "Variables/GameCore/Bool")]
    public class BoolVariable : SavableVariable<bool>
    {
        [Button]
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }

        [ShowInInspector] public string SavedValue => Tools.GetBool (name).ToString ();
        [Button]
        public override void LoadValue ()
        {
            base.LoadValue ();
            _value = Tools.GetBool (name);
        }

        [Button]
        public override void SaveValue ()
        {
            Tools.SetBool (name, Value);
        }

        public void SetValue (bool value)
        {
            Value = value;
        }

        public void SetValue (BoolVariable value)
        {
            Value = value.Value;
        }

        [ContextMenu ("Toggle")]
        public void Toggle ()
        {
            Value = !Value;
        }

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }

    }
}

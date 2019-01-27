using GameCore;

using System;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "MaterialSwitcher",
        menuName = "Variables/CyberBeat/MaterialSwitcher")]
    public class MaterialSwitcherVariable : SavableVariable<MaterialSwitcher>
    {
        public Color CurrentColor { get => ValueFast.CurrentColor; set => ValueFast.CurrentColor = value; }

        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (MaterialSwitcher value)
        {
            Value = value;
        }

        public bool ChechColor (Color color)
        {
            return ValueFast.ChechColor (color);
        }

        public void SetValue (MaterialSwitcherVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From MaterialSwitcherVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From MaterialSwitcherVariable
        }

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }

        public static implicit operator MaterialSwitcher (MaterialSwitcherVariable variable)
        {
            return variable.Value;
        }
    }
}

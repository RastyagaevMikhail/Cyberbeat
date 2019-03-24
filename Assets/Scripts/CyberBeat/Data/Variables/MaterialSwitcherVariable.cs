using GameCore;

using System;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "MaterialSwitcher",
        menuName = "CyberBeat/Variable/MaterialSwitcher")]
    public class MaterialSwitcherVariable : SavableVariable<MaterialSwitcher>
    {
        public Color CurrentColor { get => Value.CurrentColor; set => Value.CurrentColor = value; }

        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetColor (ColorVariable colorVariable)
        {
                CurrentColor = colorVariable.Value;
        }
        public void SetValue (MaterialSwitcher value)
        {
            Value = value;
        }

        public bool ChechColor (Color color)
        {
            return Value.ChechColor (color);
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

       

        public static implicit operator MaterialSwitcher (MaterialSwitcherVariable variable)
        {
            return variable.Value;
        }
    }
}

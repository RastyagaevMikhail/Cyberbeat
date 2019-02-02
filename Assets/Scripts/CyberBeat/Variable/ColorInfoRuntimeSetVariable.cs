using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "ColorInfoRuntimeSet", 
    menuName = "Variables/CyberBeat/ColorInfoRuntimeSet")]
    public class ColorInfoRuntimeSetVariable : SavableVariable<ColorInfoRuntimeSet>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (ColorInfoRuntimeSet value)
        {
            Value = value;
        }

        public void SetValue (ColorInfoRuntimeSetVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From ColorInfoRuntimeSetVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From ColorInfoRuntimeSetVariable
        }

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }
        
        public static implicit operator ColorInfoRuntimeSet (ColorInfoRuntimeSetVariable variable)
        {
            return variable.Value;
        }
    }
}


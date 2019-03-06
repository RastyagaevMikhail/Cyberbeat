using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "ColorInfoRuntimeSetRuntimeSet", 
    menuName = "Variables/CyberBeat/ColorInfoRuntimeSetRuntimeSet")]
    public class ColorInfoRuntimeSetRuntimeSetVariable : SavableVariable<ColorInfoRuntimeSetRuntimeSet>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }

        public void SetValue (ColorInfoRuntimeSetRuntimeSetVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From ColorInfoRuntimeSetRuntimeSetVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From ColorInfoRuntimeSetRuntimeSetVariable
        }

       
        
        public static implicit operator ColorInfoRuntimeSetRuntimeSet (ColorInfoRuntimeSetRuntimeSetVariable variable)
        {
            return variable.Value;
        }
    }
}


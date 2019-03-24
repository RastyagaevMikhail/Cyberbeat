using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "TrackBitItemData", 
    menuName = "CyberBeat/Variable/TrackBitItemData")]
    public class TrackBitItemDataVariable : SavableVariable<TrackBitItemData>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (TrackBitItemData value)
        {
            Value = value;
        }

        public void SetValue (TrackBitItemDataVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From TrackBitItemDataVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From TrackBitItemDataVariable
        }

       
        
        public static implicit operator TrackBitItemData (TrackBitItemDataVariable variable)
        {
            return variable.Value;
        }
    }
}


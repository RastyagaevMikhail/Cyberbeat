
using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (fileName = "TrackBitsCollection", menuName = "CyberBeat/Variable/TrackBitsCollection")]
    public class TrackBitsCollectionVariable : SavableVariable<TrackBitsCollection>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }

        public void SetValue (TrackBitsCollectionVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From TrackBitsCollectionVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From TrackBitsCollectionVariable
        }        
        public static implicit operator TrackBitsCollection (TrackBitsCollectionVariable variable)
        {
            return variable.Value;
        }
    }
}


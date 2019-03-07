using GameCore;

using Sirenix.OdinInspector;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "Track",
        menuName = "Variables/CyberBeat/Track")]
    public class TrackVariable : SavableVariable<Track>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (Track value)
        {
            Value = value;
        }

        public void SetValue (TrackVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From TrackVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From TrackVariable
        }
        public static implicit operator Track (TrackVariable variable)
        {
            return variable.Value;
        }
    }
}

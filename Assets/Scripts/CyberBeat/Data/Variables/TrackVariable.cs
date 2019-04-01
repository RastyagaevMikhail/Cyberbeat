using GameCore;

using Sirenix.OdinInspector;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "Track",
        menuName = "CyberBeat/Variable/Track")]
    public class TrackVariable : SavableVariable<Track>
    {
        [ContextMenu ("Reset Default")]
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetStartSpeedToVariable (FloatVariable variable)
        {
            variable.Value = Value.StartSpeed;
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
            if (Value) PlayerPrefs.SetString (name, Value.name);
        }

        public override void LoadValue ()
        {
            string SavedName = PlayerPrefs.GetString (name, DefaultValue ? DefaultValue.name : "Julius Dreisig - In My Head");

            string path = $"Data/Tracks/{SavedName}";
            base.Value = Resources.Load<Track> (path);
        }
        public static implicit operator Track (TrackVariable variable)
        {
            return variable.Value;
        }
    }
}

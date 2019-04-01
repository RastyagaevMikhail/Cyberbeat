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
            if (logChanges)
            {
                Debug.Log ("!!!---TrackVariable.SaveValue---!!!");
                Debug.LogFormat ("Value = {0}", Value);
            }

            if (Value) PlayerPrefs.SetString (name, Value.name);

            if (logChanges) Debug.Log ("---!!!TrackVariable.SaveValue!!!---");
        }

        public override void LoadValue ()
        {
            if (logChanges) Debug.Log ("!!!---TrackVariable.LoadValue---!!!");
            
            string SavedName = PlayerPrefs.GetString (name, DefaultValue ? DefaultValue.name : "Julius Dreisig - In My Head");

            if (logChanges) Debug.LogFormat ("SavedName = {0}", SavedName);

            string path = $"Data/Tracks/{SavedName}";
            if (logChanges) Debug.LogFormat ("path = {0}", path);
            Value = Resources.Load<Track> (path);
            if (logChanges) Debug.LogFormat ("Value = {0}", Value);

            if (logChanges) Debug.Log ("---!!!TrackVariable.LoadValue!!!---");
        }
        public static implicit operator Track (TrackVariable variable)
        {
            return variable.Value;
        }
    }
}

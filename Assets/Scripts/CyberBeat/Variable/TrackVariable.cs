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

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }

        public static implicit operator Track (TrackVariable variable)
        {
            return variable.Value;
        }

        [Button] public void ShowPath () { Debug.Log (UnityEditor.AssetDatabase.GetAssetPath (this)); }
    }
}

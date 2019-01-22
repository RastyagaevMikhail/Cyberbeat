using GameCore;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "ABitDataCollection",
        menuName = "Variables/CyberBeat/ABitDataCollection")]
    public class ABitDataCollectionVariable : SavableVariable<ABitDataCollection>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (ABitDataCollection value)
        {
            Value = value;
        }

        public void SetValue (ABitDataCollectionVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From ABitDataCollectionVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From ABitDataCollectionVariable
        }

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }

        public static implicit operator ABitDataCollection (ABitDataCollectionVariable variable)
        {
            return variable.Value;
        }
    }
}

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

       

        public static implicit operator ABitDataCollection (ABitDataCollectionVariable variable)
        {
            return variable.Value;
        }
    }
}

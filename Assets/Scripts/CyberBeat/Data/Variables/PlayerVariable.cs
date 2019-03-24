
using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "Player", 
    menuName = "CyberBeat/Variable/Player")]
    public class PlayerVariable : SavableVariable<Player>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }

        public void SetValue (PlayerVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From PlayerVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From PlayerVariable
        }        
        public static implicit operator Player (PlayerVariable variable)
        {
            return variable.Value;
        }
    }
}


using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "MenuWindowType", 
    menuName = "CyberBeat/Variable/MenuWindowType")]
    public class MenuWindowTypeVariable : SavableVariable<MenuWindowType>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (MenuWindowType value)
        {
            Value = value;
        }

        public void SetValue (MenuWindowTypeVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From MenuWindowTypeVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From MenuWindowTypeVariable
        }

       
        
        public static implicit operator MenuWindowType (MenuWindowTypeVariable variable)
        {
            return variable.Value;
        }
    }
}


using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "MenuWindowType", 
    menuName = "Variables/CyberBeat/MenuWindowType")]
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

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }
        
        public static implicit operator MenuWindowType (MenuWindowTypeVariable variable)
        {
            return variable.Value;
        }
    }
}


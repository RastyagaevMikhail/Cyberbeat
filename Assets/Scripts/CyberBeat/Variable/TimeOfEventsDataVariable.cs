using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    [CreateAssetMenu (
        fileName = "TimeOfEventsData", 
    menuName = "Variables/CyberBeat/TimeOfEventsData")]
    public class TimeOfEventsDataVariable : SavableVariable<TimeOfEventsData>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (TimeOfEventsData value)
        {
            Value = value;
        }

        public void SetValue (TimeOfEventsDataVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From TimeOfEventsDataVariable
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From TimeOfEventsDataVariable
        }

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }
        
        public static implicit operator TimeOfEventsData (TimeOfEventsDataVariable variable)
        {
            return variable.Value;
        }
    }
}


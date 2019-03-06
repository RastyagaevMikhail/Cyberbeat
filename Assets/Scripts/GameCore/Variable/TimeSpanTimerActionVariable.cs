using System;
using GameCore;
using UnityEngine;
namespace  GameCore
{
    [CreateAssetMenu (
        fileName = "TimeSpanTimerAction", 
    menuName = "Variables/GameCore/TimeSpanTimerAction")]
    public class TimeSpanTimerActionVariable : SavableVariable<TimeSpanTimerAction>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (TimeSpanTimerAction value)
        {
            Value = value;
        }

        public void SetValue (TimeSpanTimerActionVariable value)
        {
            Value = value.Value;
        }

        public void StartTimer()
        {
            ValueFast.StartTimer();
        }
        

        public override void SaveValue ()
        {
            // TODO: Save Code This From TimeSpanTimerActionVariable
        }

        public void ResetToDefault()
        {
            ValueFast.ResetToDefault();
        }

        public override void LoadValue ()
        {
          // TODO: Load Code This From TimeSpanTimerActionVariable
        }

       
        
        public static implicit operator TimeSpanTimerAction (TimeSpanTimerActionVariable variable)
        {
            return variable.Value;
        }
    }
}


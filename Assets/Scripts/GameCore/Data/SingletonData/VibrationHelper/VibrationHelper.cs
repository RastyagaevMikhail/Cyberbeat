using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    public class VibrationHelper : SingletonData<VibrationHelper>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/Vibration")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void InitOnCreate () { }
        public override void ResetDefault () { }
#endif
        public void Vibrate ()
        {
            Vibration.Vibrate ();
        }
        public void Vibrate (long milliseconds)
        {
            Vibration.Vibrate (milliseconds);
        }
        public void Vibrate (float seconds)
        {
            Vibration.Vibrate (seconds);
        }

        public void Vibrate (long[] pattern, int repeat)
        {
            Vibration.Vibrate (pattern, repeat);
        }
        public void VibrrateWithAmlitude (long milliseconds, int amplitude)
        {
            Vibration.VibrrateWithAmlitude (milliseconds, amplitude);
        }
        public bool HasVibrator ()
        {
            return Vibration.HasVibrator ();
        }
        public void Cancel ()
        {
            Vibration.Cancel ();
        }
    }
}

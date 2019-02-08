using GameCore;

using System;

using UnityEngine;

namespace CyberBeat
{
    [Serializable]
    public class ProgressInfo
    {
        [SerializeField] IntVariable best;
        public IntVariable BestVariable => best;
        public float Best { get { return best.Value; } private set { best.Value = (int) value; } }

        [SerializeField] IntVariable max;
        public IntVariable MaxVariable => max;
        public float Max
        {
            get { return max.Value; }
#if !UNITY_EDITOR
            private
#endif
            set { max.Value = (int) value; }
        }

        [SerializeField] FloatVariable percent;
        public float Percent { get { return percent.Value; } private set { percent.Value = (int) value; } }

        public void Progress (int Current)
        {
            if (Current > Best)
            {
                Best = Current;
                Percent = Best / Max;
            }
        }

#if UNITY_EDITOR
        public void Validate (string nameTrack)
        {

            best = ValidateVariable<IntVariable> ("Assets/Data/Tracks/ProgressInfo/{0}/Best_{0}.asset".AsFormat (nameTrack), true);
            // best.Validate ("Assets/Data/Tracks/ProgressInfo/{0}/Best_{0}.asset".AsFormat (nameTrack), true);
            // Debug.Log (best, best);

            max = ValidateVariable<IntVariable> ("Assets/Data/Tracks/ProgressInfo/{0}/Max_{0}.asset".AsFormat (nameTrack));

            percent = ValidateVariable<FloatVariable> ("Assets/Data/Tracks/ProgressInfo/{0}/Percent_{0}.asset".AsFormat (nameTrack), true);
        }
        public T ValidateVariable<T> (string path, bool isSavable = false) where T : ASavableVariable
        {
            T variable = Tools.GetAssetAtPath<T> (path);
            if (variable == null)
            {
                variable = ScriptableObject.CreateInstance<T> ();
                variable.CreateAsset (path, isSavable);
            }

            return variable;
        }
        public void Save ()
        {
            max.Save ();
        }
#endif

        public int AsPercent (IntVariable intValue)
        {
            return percent.AsPercent (intValue);
        }
    }
}

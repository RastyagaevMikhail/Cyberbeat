using GameCore;

using System;

using UnityEngine;

namespace CyberBeat
{
    [Serializable]
    public class ProgressInfo
    {
        [SerializeField] IntVariable best;
        private float Best { get { return best.Value; } set { best.Value = (int) value; } }

        [SerializeField] IntVariable max;
        public float Max => max.Value;

        public IntVariable[] progressVariables => new IntVariable[] { best, max };
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
        public void Validate (string nameTrack, int max = 0)
        {

            best = ValidateVariable<IntVariable> ($"Assets/Data/Tracks/ProgressInfo/{nameTrack}/Best_{nameTrack}.asset", true);
            // best.Validate ("Assets/Data/Tracks/ProgressInfo/{0}/Best_{0}.asset".AsFormat (nameTrack), true);
            // Debug.Log (best, best);

            this.max = ValidateVariable<IntVariable> ($"Assets/Data/Tracks/ProgressInfo/{nameTrack}/Max_{nameTrack}.asset");
            this.max.Value = max;
            this.max.Save ();
            percent = ValidateVariable<FloatVariable> ($"Assets/Data/Tracks/ProgressInfo/{nameTrack}/Percent_{nameTrack}.asset", true);
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

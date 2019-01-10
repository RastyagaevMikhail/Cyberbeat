using GameCore;

using System;
using UnityEngine;

namespace CyberBeat
{
    [Serializable]
    public class ProgressInfo
    {
        public IntVariable Best;
        public IntVariable Max;
        public FloatVariable Percent;

        public void Progress (int Current)
        {
            if (Current > Best.Value) Best.Value = Current;
            Percent.Value = Best.AsFloat () / Max.AsFloat ();
        }

        public void Generate (string nameTrack)
        {
            Best = ScriptableObject.CreateInstance<IntVariable>();
            Best.CreateAsset ("Assets/Data/Tracks/ProgressInfo/{0}/Best_{0}.asset".AsFormat(nameTrack), true);

            Max = ScriptableObject.CreateInstance<IntVariable>();
            Max.CreateAsset ("Assets/Data/Tracks/ProgressInfo/{0}/Max_{0}.asset".AsFormat(nameTrack));

            Percent = ScriptableObject.CreateInstance<FloatVariable>();
            Percent.CreateAsset ("Assets/Data/Tracks/ProgressInfo/{0}/Percent_{0}.asset".AsFormat(nameTrack), true);
        }
    }
}

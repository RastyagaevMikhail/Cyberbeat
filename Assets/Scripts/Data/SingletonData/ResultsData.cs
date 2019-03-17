using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class ResultsData : SingletonData<ResultsData>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/ResultsData")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void InitOnCreate () { }
        public override void ResetDefault () { Reset (); }
#else
        public override void ResetDefault () { }
#endif
        [SerializeField] IntVariable attemps;
        [SerializeField] IntVariable totalBits;
        [SerializeField] IntVariable totalNotes;
        [SerializeField] IntVariable currentScore;
        [SerializeField] IntVariable currentMaxScore;
        [SerializeField] IntVariable scorePerBeat;
        [SerializeField] IntVariable notes;

        [SerializeField] BoolVariable DoubleCoins;
        [SerializeField] IntVariable doubleReward;
        [SerializeField] IntVariable reward;

        bool doubleCoins { get { return DoubleCoins.Value; } }
        int notesPerBeat { get { return (doubleCoins ? 2 : 1) * scorePerBeat.Value; } }

        float onLastPausedTime;
        public float OnLastPausedTime { set => onLastPausedTime = value; }

        [SerializeField] TrackVariable trackVariable;
        private float trackLengthTime => trackVariable.Value.music.clip.length;

        public ProgressInfo progressInfo { get { return trackVariable.Value.progressInfo; } }

        public void AccumulateAttems ()
        {
            attemps.Increment ();
            currentScore.ResetDefault ();
        }

        public void CalculateTotalPercent ()
        {
            float timePercent = onLastPausedTime / trackLengthTime;
            float beatPercent = currentScore.Value / progressInfo.Max;
            totalPercent += ((timePercent + beatPercent) / 2);
        }

        public void AccumulateBeatsNotes ()
        {
            //Bits
            totalBits.Increment ();
            currentScore.Increment ();
            progressInfo.Progress (currentScore.Value);
            //Notes
            totalNotes.ApplyChange (notesPerBeat);
        }
        public int DoubleReward => Reward + (int) (0.5f * Reward);
        // totalBits.Value +
        // progressInfo.AsPercent (totalBits) +
        // totalNotes.Value;

        public void TakeReward (bool doubleReward)
        {
            notes.ApplyChange (doubleReward ? DoubleReward : Reward);
        }

        public int Reward;
        public void Reset ()
        {
            attemps.ResetDefault ();
            totalBits.ResetDefault ();
            totalNotes.ResetDefault ();
            scorePerBeat.ResetDefault ();
            totalPercent = 0;
            onLastPausedTime = 0;
        }
        float totalPercent = 0;
        public void Calculate ()
        {
            if (attemps.Value == 1)
            {
                float beatPercent = currentScore.Value / progressInfo.Max;
                Reward = (int) (trackVariable.Value.maxReward * beatPercent);
            }
            else
            {
                CalculateTotalPercent ();
                Reward = (int) ((totalPercent / attemps.Value) * trackVariable.Value.maxReward);
            }

            reward.Value = Reward;
            doubleReward.Value = DoubleReward;

            currentMaxScore.Value = (int) progressInfo.Max;

        }
    }
}

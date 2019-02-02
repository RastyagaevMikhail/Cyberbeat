using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat {
    public class ResultsData : SingletonData<ResultsData> {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Game/Data/ResultsData")] public static void Select() { UnityEditor.Selection.activeObject = instance; }
        public override void InitOnCreate() { }
        public override void ResetDefault() { Reset(); }
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
        int notesPerBeat { get { return (doubleCoins ? 2 : 1) * scorePerBeat.ValueFast; } }

        [SerializeField] TrackVariable trackVariable;
        public ProgressInfo progressInfo { get { return trackVariable.ValueFast.progressInfo; } }

        public void AccumulateAttems() {
            attemps.Increment();
            currentScore.ResetDefault();
        }
        public void AccumulateBeatsNotes() {
            //Bits
            totalBits.Increment();
            currentScore.Increment();
            progressInfo.Progress(currentScore.Value);
            //Notes
            totalNotes.ApplyChange(notesPerBeat);
        }
        public int DoubleReward =>
        totalBits.Value +
        progressInfo.AsPercent(totalBits) +
        totalNotes.Value;

        public void TakeReward(bool doubleReward) {
            notes.ApplyChange(doubleReward ? DoubleReward : Reward);
            Reset();
        }

        public int Reward => DoubleReward / 2;
        public void Reset() {
            attemps.ResetDefault();
            totalBits.ResetDefault();
            totalNotes.ResetDefault();
            scorePerBeat.ResetDefault();
        }
        public void Calculate() {
            reward.ValueFast = Reward;
            doubleReward.ValueFast = DoubleReward;

            currentMaxScore.Value = (int) progressInfo.Max;
        }
    }
}

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
        [SerializeField] IntVariable _currentScore;
        public int currentScore { get => _currentScore.Value; set => _currentScore.Value = value; }

        [SerializeField] IntVariable _BestInSessionScore;
        public int bestInSessionScore { get => _BestInSessionScore.Value; set => _BestInSessionScore.Value = value; }

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
            currentScore = 0;
        }

        public void AccumulateBeatsNotes ()
        {
            //Bits
            totalBits.Increment ();
            _currentScore.Increment ();

            if (bestInSessionScore < currentScore)
                bestInSessionScore = currentScore;
            progressInfo.Progress (currentScore);
            //Notes
            totalNotes.ApplyChange (notesPerBeat);
        }
        public int DoubleReward => Reward + (int) (0.5f * Reward);
        // totalBits.Value +
        // progressInfo.AsPercent (totalBits) +
        // totalNotes.Value;

        public void TakeReward (bool doubleReward)
        {
            int amount = doubleReward ? DoubleReward : Reward;
            amount = amount.GetAsClamped (1, amount);
            notes.ApplyChange (amount);
        }
        public float completePercent { get => _CompletePercent.Value; private set => _CompletePercent.Value = value; }
        public void Reset ()
        {
            attemps.ResetDefault ();
            totalBits.ResetDefault ();
            totalNotes.ResetDefault ();
            scorePerBeat.ResetDefault ();
            _BestInSessionScore.ResetDefault ();
            _CompletePercent.ResetDefault ();
            _TotalPercent = 0;
            onLastPausedTime = 0;
            CompleteTrack = false;
        }

        [SerializeField] float _TotalPercent = 0;
        [SerializeField] FloatVariable _CompletePercent;
        public bool CompleteTrack { get; set; }

        [SerializeField] bool debug;
        public void CalculateTotalPercent ()
        {
            if (debug)
            {
                Debug.LogFormat ("CompleteTrack = {0}", CompleteTrack);
                Debug.LogFormat ("onLastPausedTime = {0}", onLastPausedTime);
                Debug.LogFormat ("trackLengthTime = {0}", trackLengthTime);
            }
            float timePercent = (CompleteTrack ? 1f : (onLastPausedTime / trackLengthTime));
            if (debug)
            {
                Debug.LogFormat ("timePercent = {0}", timePercent);
                Debug.LogFormat ("bestInSessionScore = {0}", bestInSessionScore);
                Debug.LogFormat ("progressInfo.Max = {0}", progressInfo.Max);
            }
            float beatPercent = bestInSessionScore / progressInfo.Max;
            if (debug)
            {
                Debug.LogFormat ("beatPercent = {0}", beatPercent);
                Debug.LogFormat ("totalPercent = {0}", _TotalPercent);
            }
            _TotalPercent += ((timePercent + beatPercent) / 2f);
            if (debug) Debug.LogFormat ("totalPercent = {0}", _TotalPercent);

        }
        int maxReward => trackVariable.Value.maxReward;
        int Reward => (int) (completePercent.GetAsClamped (0, 1f) * maxReward).GetAsClamped (1, maxReward);
        public void Calculate ()
        {
            CalculateTotalPercent ();

            completePercent = (_TotalPercent / attemps.Value.log (debug, "Value"));

            completePercent.log (debug, "completePercent");
            reward.Value = Mathf.Clamp (Reward, 1, maxReward);;
            doubleReward.Value = DoubleReward;

            currentMaxScore.Value = (int) progressInfo.Max;
        }
    }
}

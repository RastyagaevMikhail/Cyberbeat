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
        public override void InitOnCreate ()
        {

        }

        public override void ResetDefault ()
        {
            Reset ();
        }
#endif
        [SerializeField] IntVariable Attemps;
        [SerializeField] IntVariable AccumulatedBits;
        [SerializeField] IntVariable AccumulatedNotes;
        [SerializeField] int CurrentScore;
        [SerializeField] IntVariable ScorePerBeat;
        [SerializeField] BoolVariable IsCombo;
        [SerializeField] BoolVariable DoubleCoins;
        [SerializeField] FloatVariable BoostersUsePercent;
        [Header ("Combo")]
        [SerializeField] IntVariable CurrentComboBeatsCount;
        [SerializeField] IntVariable CurrentComboMaxCount;
        [SerializeField] IntVariable CountCombo;
        [SerializeField] IntVariable doubleReward;
        [SerializeField] IntVariable reward;
        [SerializeField] FloatVariable ComboPercent;
        [SerializeField] FloatVariable AccumulatedComboPercent;

        bool isCombo { get { return IsCombo.Value; } }
        int scorePerBeat { get { return ScorePerBeat.Value; } }
        bool doubleCoins { get { return DoubleCoins.Value; } }
        int notesPerBeat { get { return (doubleCoins ? 2 : 1) * scorePerBeat; } }
        public TracksCollection tracksCollcetion { get { return TracksCollection.instance; } }
        public Track track { get { return tracksCollcetion.CurrentTrack; } }

        public void AccumulateAttems ()
        {
            Attemps.Increment ();
            CurrentScore = 0;
        }
        public void AccumulateBeatsNotes ()
        {
            //Bits
            AccumulatedBits.Increment ();
            CurrentScore++;
            track.progressInfo.Progress (CurrentScore);
            //Notes
            AccumulatedNotes.ApplyChange (notesPerBeat);
        }
        public void AccumulateBoosterUsePercent (float boosterUsePercent)
        {
            BoostersUsePercent.ApplyChange (boosterUsePercent);
        }

        public void TakeDoubleReward ()
        {
            GameData.instance.Notes.ApplyChange (DoubleReward);
        }

        public int DoubleReward
        {
            get
            {
                return AccumulatedBits.Value +
                    BoostersUsePercent.AsPercent (AccumulatedBits) +
                    ComboPercent.AsPercent (AccumulatedBits) +
                    track.progressInfo.AsPercent (AccumulatedBits) +
                    AccumulatedNotes.Value;
            }
        }

        public void TakeReward ()
        {
            GameData.instance.Notes.ApplyChange (Reward);
        }

        public int Reward
        {
            get
            {
                return DoubleReward / 2;
            }
        }

        public void Reset ()
        {
            Attemps.ResetDefault ();
            AccumulatedBits.ResetDefault ();
            AccumulatedNotes.ResetDefault ();
            ScorePerBeat.ResetDefault ();

            BoostersUsePercent.ResetDefault ();

            CurrentComboBeatsCount.ResetDefault ();
            CurrentComboMaxCount.ResetDefault ();
            CountCombo.ResetDefault ();
            doubleReward.ResetDefault ();
            reward.ResetDefault ();

            ComboPercent.ResetDefault ();
            AccumulatedComboPercent.ResetDefault ();
        }
        public void AccumulateComboData (bool isCombo)
        {
            if (isCombo)
            {
                CountCombo.Increment ();
            }
            else
            {
                AccumulateComboPercent ();
            }
        }

        private void AccumulateComboPercent ()
        {
            float comboPercent = CurrentComboBeatsCount.AsFloat () / CurrentComboMaxCount.AsFloat ();
            AccumulatedComboPercent.ApplyChange (comboPercent);
        }

        public void Calculate ()
        {
            if (isCombo) AccumulateComboPercent ();
            ComboPercent.Value = CountCombo.Value == 0 ? 0 : AccumulatedComboPercent.Value / CountCombo.AsFloat ();
            reward.SetValue (Reward);
            doubleReward.SetValue (DoubleReward);
        }
    }
}

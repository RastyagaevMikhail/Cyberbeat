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
        [SerializeField] IntVariable ScorePerBeat;
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

        int scorePerBeat { get { return ScorePerBeat.Value; } }
        bool doubleCoins { get { return DoubleCoins.Value; } }
        int notesPerBeat { get { return (doubleCoins ? 2 : 1) * scorePerBeat; } }

        public void AccumulateAttems ()
        {
            Attemps.Increment ();
        }
        public void AccumulateBeatsNotes ()
        {
            //Bits
            AccumulatedBits.Increment ();
            //Notes
            AccumulatedNotes.ApplyChange (notesPerBeat);
        }
        public void AccumulateBoosterData (BoosterData boosterData)
        {
            BoostersUsePercent.ApplyChange (boosterData.boosterUsePercent);
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
                    ComboPercent.AsPercent (AccumulatedBits);
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
                float comboPercent = CurrentComboBeatsCount.AsFloat () / CurrentComboMaxCount.AsFloat ();
                AccumulatedComboPercent.ApplyChange (comboPercent);
            }
        }
        public void Calculate ()
        {
            ComboPercent.Value = CountCombo.Value == 0 ? 0 : AccumulatedComboPercent.Value / CountCombo.AsFloat ();
            reward.SetValue (Reward);
            doubleReward.SetValue (DoubleReward);
        }
    }
}

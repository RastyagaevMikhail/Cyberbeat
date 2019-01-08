using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [RequireComponent (typeof (GameEventListenerColorInterractor))]
    [RequireComponent (typeof (GameEventListenerBoosterData))]
    [RequireComponent (typeof (OnBoolVariableChanged))]
    public class ResultsController : MonoBehaviour
    {
        public ResultsData data { get { return ResultsData.instance; } }
        
        void Start ()
        {
            data.AccumulateAttems ();
        }
        public void OnColorInterractorDeath (ColorInterractor interractor)
        {
            data.AccumulateBeatsNotes ();
        }
        public void OnBoosterUse (BoosterData boosterData)
        {
            data.AccumulateBoosterData (boosterData);
        }
        public void _OnChanged (bool isCombo)
        {
            data.AccumulateComboData(isCombo);
        }
    }
}

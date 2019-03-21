using GameCore;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    public abstract class BitTimeItem<TBitData> : ATimeUpdateable<ABitDataCollectionVariable, ABitDataCollection, UnityEventIBitData, IBitData> where TBitData : IBitData
    {
        List<IBitData> Bits { get { return Variable.Value.Bits; } }
        private IEnumerable<ITimeItem> timeItems = null;
        public override IEnumerable<ITimeItem> TimeItems => Bits;

        private IEnumerable<ITimeItem> InitItems ()
        {
            try
            {
                timeItems = Bits;
                return timeItems;
            }
            catch (System.Exception)
            {
                return new List<ITimeItem> ();
            }

        }
      /*   private void OnEnable ()
        {
            InitItems ();
        } */

        public override ITimeItem CurrentTimeItem
        {
            get => currentBit;
            set => currentBit = value as IBitData;
        }
        IBitData currentBit;
        [SerializeField] UnityEventIBitData onStart;
        public override void Start ()
        {
            base.Start ();
            onStart.Invoke (currentBit);
        }
        public override void UpdateInTime (float time)
        {
            if (TimesIsOver) return;

            if (currentBit == null || Bits.Count == 0) return; //??? ЧТо делать не заню с этим, вроде работает
            if (currentBit.StartTime <= time)
            {
                UnityEvent.Invoke (currentBit);

                indexOfTime++;
                if (TimesIsOver)
                {
                    OnTimeIsOver ();
                    return;
                }
                try
                {
                    currentBit = Bits[indexOfTime];

                }
                catch (System.Exception)
                {
                    // Debug.Log ($"{Bits.Count} == {indexOfTime}, count == i  ", this);
                    // throw;
                }
                // CurrentTimeItem = TimeItems.ElementAt(indexOfTime);
            }
        }
        public override string ToString ()
        {
            return $"{Variable} {(Variable ? Variable.Value:default(ABitDataCollection) )}";
        }
    }
}

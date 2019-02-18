using GameCore;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    public abstract class BitTimeItem<TBitData> : ATimeUpdateable<ABitDataCollectionVariable, ABitDataCollection, GameEventListenerIBitData.UnityEventIBitData, IBitData> where TBitData : IBitData
    {
        List<IBitData> Bits { get { return Variable.ValueFast.Bits; } }
        private IEnumerable<ITimeItem> timeItems = null;
        public override IEnumerable<ITimeItem> TimeItems => timeItems??InitItems ();

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
        private void OnEnable ()
        {
            InitItems ();
        }

        public override ITimeItem CurrentTimeItem
        {
            get => currentBit;
            set => currentBit = value as IBitData;
        }
        IBitData currentBit;
        public override void UpdateInTime (float time)
        {
            if (TimesIsOver) return;

            if (currentBit == null) return;//??? ЧТо делать не заню с этим, вроде работает
            if (currentBit.StartTime <= time)
            {
                UnityEvent.Invoke (currentBit);

                indexOfTime++;
                if (TimesIsOver)
                {
                    OnTimeIsOver ();
                    return;
                }
                currentBit = Bits[indexOfTime];
                // CurrentTimeItem = TimeItems.ElementAt(indexOfTime);
            }
        }
        public override string ToString ()
        {
            return $"{Variable}";
        }
    }
}

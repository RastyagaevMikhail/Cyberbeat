using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    using System;

    using UnityEngine;

    // [CreateAssetMenu(fileName = "ABayable", menuName = "Cyberbeat/ABayable", order = 0)]
    public abstract class ABayable : ScriptableObject
    {
        [Header ("Bayable Data")]
        public Sprite Icon;
        public string title;
        public string Description;
        [SerializeField] GameEventBayable IsOver;
    
        public bool TryBuy ()
        {
            bool canBuy = Buyer.TryBuyDefaultCurency (Price);
            if (canBuy) Increment ();
            return canBuy;
        }
        public IntVariable Count;
        public int Price;
        public void Increment ()
        {
            Count.Increment ();
        }
        public bool TryUse (Action OnCanUse = null)
        {
            bool canUse = Count.Value > 0;
            if (canUse)
            {
                Count.Decrement ();
                if (OnCanUse != null) OnCanUse ();
            }
            else
                IsOver.Raise (this);
            return canUse;
        }
    }
}

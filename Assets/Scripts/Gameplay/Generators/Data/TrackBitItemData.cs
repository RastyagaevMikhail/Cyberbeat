using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using static CyberBeat.GameEventListenerIBitData;

namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/ATimeUpdateable/BitTimeItem/TrackBitItemData")]
    public class TrackBitItemData : BitTimeItem<TrackBit>
    {
        [SerializeField] UnityEventIBitData TimeIsOver;
     
        public override void OnTimeIsOver ()
        {
            base.OnTimeIsOver ();
            if (TimeItems.Count () > 0)
                TimeIsOver.Invoke (Variable.Value.Bits.Last ());
        }
    }
}

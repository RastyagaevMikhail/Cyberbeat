using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/ATimeUpdateable/BitTimeItem/TrackBitItemData")]
    public class TrackBitItemData : BitTimeItem<TrackBit>
    {
        [SerializeField] UnityEventFloat TimeIsOver;
        protected override void OnTimeIsOver ()
        {
            base.OnTimeIsOver ();
            TimeIsOver.Invoke (TimeItems.Count () > 0 ? (TimeItems.Last () as IBitData).StartTime : 0f);
        }
    }
}

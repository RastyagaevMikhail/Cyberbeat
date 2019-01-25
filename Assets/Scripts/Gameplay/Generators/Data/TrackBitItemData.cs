using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/ATimeUpdateable/BitTimeItem/TrackBitItemData")]
    public class TrackBitItemData : BitTimeItem<TrackBit>
    {
        protected override void OnTimeIsOver ()
        {
            base.OnTimeIsOver ();
        }
    }
}
